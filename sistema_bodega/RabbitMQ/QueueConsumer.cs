using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sistema_bodega.Data;

namespace sistema_bodega.RabbitMQ
{
    /// <summary>
    /// Microservicio que se encarga de recepcionar y procesar los mensajes
    /// desde RabbitMQ
    /// </summary>
    public class QueueConsumer : BackgroundService
    {
        private IServiceProvider _sp;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        /// <summary>
        /// En el constructor realizamos la configuracion inicial
        /// </summary>
        /// <param name="sp"></param>
        public QueueConsumer(IServiceProvider sp)
        {
            _sp = sp;

            // Las credenciales para conectarse al servidor RabbitMQ
            _factory = new ConnectionFactory()
            {
                HostName = "192.168.1.119",
                UserName = "bodega",
                Password = "bodega",
                Port = 5672,
                VirtualHost = "test"
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Indicamos el Exchange que vamos a usar
            _channel.ExchangeDeclare(exchange: "mantencion",
                                        type: "direct");

            // Obtenemos el nombre de la cola desde el Exchange
            _queueName = _channel.QueueDeclare().QueueName;

            // Nos enlasamos a la cola
            _channel.QueueBind(queue: _queueName,
                              exchange: "mantencion",
                              routingKey: "bodega");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Segun internet esto es para evitar fugas, ¿de que?, no lo se
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            // Se crea el consumidor
            var consumer = new EventingBasicConsumer(_channel);

            // Aqui se define que se hará una vez se reciba el mensaje, que es actualizar la base de datos
            consumer.Received += (model, ea) =>
            {
                // El cuerpo del mensaje
                var body = ea.Body.ToArray();

                // Utilizamos nuestro DecodeHelper para descifrar el JSON
                DecodeHelper decodeHelper = JsonConvert.DeserializeObject<DecodeHelper>(Encoding.UTF8.GetString(body));

                // Ahora incluimos todo el proceso de actualizar la base de datos en la pool del hilo de tareas
                Task.Run(async () =>
                {
                    // Por alguna razon para usar el DbContext es necesario crea un...Scope
                    using (var scope = _sp.CreateScope())
                    {
                        // Obtenemos la instancia de nuestra base de datos
                        BaseDatos baseDatos = scope.ServiceProvider.GetRequiredService<BaseDatos>();
                        // Primero verificamos si existe el producto
                        Producto producto = await baseDatos.Productos.FirstOrDefaultAsync(p => p.Id == decodeHelper.Id);

                        if (producto == null)
                        {
                            return;
                        }

                        ProductoRetiro productoRetiros = await baseDatos.ProductosRetiros
                            .FirstOrDefaultAsync(pr => pr.ProductoId == decodeHelper.Id && pr.Fecha == decodeHelper.Fecha);

                        // Si no existe se crea la relacion
                        if (productoRetiros == null)
                        {
                            productoRetiros = new ProductoRetiro();
                            productoRetiros.ProductoId = decodeHelper.Id;
                            productoRetiros.Fecha = decodeHelper.Fecha;
                            productoRetiros.Cantidad = decodeHelper.Cantidad;

                            baseDatos.ProductosRetiros.Add(productoRetiros);
                        }
                        // Si existe se actualiza
                        else
                        {
                            productoRetiros.Cantidad += decodeHelper.Cantidad;
                        }

                        // Se guardan los cambios
                        await baseDatos.SaveChangesAsync();

                        // Se actualiza el stock del producto en bodega
                        producto.Cantidad -= decodeHelper.Cantidad;

                        // Se guardan los cambios nuevamente
                        await baseDatos.SaveChangesAsync();
                    }
                });
            };

            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Clase auxiliar para decodificar el JSON del mensaje 
    /// </summary>
    public class DecodeHelper
    {
        public DateTime Fecha { get; set; }
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }
}
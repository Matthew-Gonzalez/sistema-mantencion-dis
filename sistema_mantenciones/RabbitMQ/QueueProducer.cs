using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System;
using System.Threading;
using sistema_mantenciones.Data;

namespace sistema_mantenciones.RabbitMQ
{
    public static class QueueProducer
    {
        /// <summary>
        /// Alerta de que un empleado ha trabajado en una mantencion
        /// </summary>
        /// <param name="fecha_mantencion">La fecha en que se realizo la mantencion</param>
        /// <param name="mantencionEmpleado">La relacion entre el empleado y la mantencion</param>
        public static void PublicarMantencion(DateTime fecha_mantencion, MantencionEmpleado mantencionEmpleado)
        {
            // Definimos nuestros parametros de conexion
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.119",
                UserName = "mantencion",
                Password = "mantencion",
                Port = 5672,
                VirtualHost = "test"
            };

            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                // Declaramos nuestro exchange
                channel.ExchangeDeclare(exchange: "mantencion",
                                        type: "direct");

                // Daremos a nuestro mensaje una estructura JSON
                var message = new
                {
                    Fecha = fecha_mantencion,
                    Rut = mantencionEmpleado.EmpleadoRut,
                    Horas = mantencionEmpleado.Horas
                };

                // Serializamos nuestro mensaje en Bytes para que RabbitMQ lo acepte
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                // Publicamos nuestro mensaje al exchange con la routing key rrhhh
                channel.BasicPublish(exchange: "mantencion",
                                     routingKey: "rrhh",
                                     basicProperties: null,
                                     body: body);
            }
        }

        /// <summary>
        /// Alerta de que se ha usado un producto en una mantencion
        /// </summary>
        /// <param name="fecha_mantencion">La fecha en que se realizo la mantencion</param>
        /// <param name="mantencionProducto">La relacion entre el producto y la mantencion</param>
        public static void PublicarMantencion(DateTime fecha_mantencion, MantencionProducto mantencionProducto)
        {
            // Definimos nuestros parametros de conexion
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.119",
                UserName = "mantencion",
                Password = "mantencion",
                Port = 5672,
                VirtualHost = "test"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declaramos nuestro exange
                channel.ExchangeDeclare(exchange: "mantencion",
                                        type: "direct");

                // Damos a nuestro mensaje un formato JSON
                var message = new
                {
                    Fecha = fecha_mantencion,
                    Id = mantencionProducto.ProductoId,
                    Cantidad = mantencionProducto.Cantidad
                };

                // Serializamos nuestro JSON en Bytes para que RabbitMQ los acepte
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                // Publicamos nuestro mensaje al exchange con la routing key bodega
                channel.BasicPublish(exchange: "mantencion",
                                     routingKey: "bodega",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
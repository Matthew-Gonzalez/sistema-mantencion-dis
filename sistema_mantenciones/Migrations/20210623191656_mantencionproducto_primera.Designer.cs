﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sistema_mantenciones.Data;

namespace sistema_mantenciones.Migrations
{
    [DbContext(typeof(BaseDatos))]
    [Migration("20210623191656_mantencionproducto_primera")]
    partial class mantencionproducto_primera
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("sistema_mantenciones.Data.Empleado", b =>
                {
                    b.Property<string>("Rut")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Rut");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("sistema_mantenciones.Data.Mantencion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mantenciones");
                });

            modelBuilder.Entity("sistema_mantenciones.Data.MantencionProducto", b =>
                {
                    b.Property<int>("MantencionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("MantencionId", "ProductoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("MantencionProducto");
                });

            modelBuilder.Entity("sistema_mantenciones.Data.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("sistema_mantenciones.Data.MantencionProducto", b =>
                {
                    b.HasOne("sistema_mantenciones.Data.Mantencion", "Mantencion")
                        .WithMany("MantencionProductos")
                        .HasForeignKey("MantencionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sistema_mantenciones.Data.Producto", "Producto")
                        .WithMany("MantencionesProducto")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mantencion");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("sistema_mantenciones.Data.Mantencion", b =>
                {
                    b.Navigation("MantencionProductos");
                });

            modelBuilder.Entity("sistema_mantenciones.Data.Producto", b =>
                {
                    b.Navigation("MantencionesProducto");
                });
#pragma warning restore 612, 618
        }
    }
}

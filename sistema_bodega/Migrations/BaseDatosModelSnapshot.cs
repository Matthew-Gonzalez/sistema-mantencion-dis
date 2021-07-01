﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sistema_bodega.Data;

namespace sistema_bodega.Migrations
{
    [DbContext(typeof(BaseDatos))]
    partial class BaseDatosModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("sistema_bodega.Data.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("sistema_bodega.Data.ProductoRetiro", b =>
                {
                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("ProductoId", "Fecha");

                    b.ToTable("ProductosRetiros");
                });

            modelBuilder.Entity("sistema_bodega.Data.ProductoRetiro", b =>
                {
                    b.HasOne("sistema_bodega.Data.Producto", "Producto")
                        .WithMany("ProductoRetiros")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("sistema_bodega.Data.Producto", b =>
                {
                    b.Navigation("ProductoRetiros");
                });
#pragma warning restore 612, 618
        }
    }
}
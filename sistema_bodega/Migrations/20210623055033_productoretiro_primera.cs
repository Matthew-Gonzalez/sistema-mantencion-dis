using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_bodega.Migrations
{
    public partial class productoretiro_primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductosRetiros",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosRetiros", x => new { x.ProductoId, x.Fecha });
                    table.ForeignKey(
                        name: "FK_ProductosRetiros_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosRetiros");
        }
    }
}

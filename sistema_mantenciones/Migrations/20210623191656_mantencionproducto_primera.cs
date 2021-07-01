using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_mantenciones.Migrations
{
    public partial class mantencionproducto_primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MantencionProducto",
                columns: table => new
                {
                    MantencionId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantencionProducto", x => new { x.MantencionId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_MantencionProducto_Mantenciones_MantencionId",
                        column: x => x.MantencionId,
                        principalTable: "Mantenciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MantencionProducto_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MantencionProducto_ProductoId",
                table: "MantencionProducto",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MantencionProducto");
        }
    }
}

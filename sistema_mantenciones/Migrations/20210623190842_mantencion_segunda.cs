using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_mantenciones.Migrations
{
    public partial class mantencion_segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Mantenciones",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Mantenciones");
        }
    }
}

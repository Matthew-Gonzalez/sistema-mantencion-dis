using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_mantenciones.Migrations
{
    public partial class mantencionempleado_primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MantencionEmpleado",
                columns: table => new
                {
                    MantencionId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoRut = table.Column<int>(type: "int", nullable: false),
                    EmpleadoRut1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Horas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantencionEmpleado", x => new { x.MantencionId, x.EmpleadoRut });
                    table.ForeignKey(
                        name: "FK_MantencionEmpleado_Empleados_EmpleadoRut1",
                        column: x => x.EmpleadoRut1,
                        principalTable: "Empleados",
                        principalColumn: "Rut",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MantencionEmpleado_Mantenciones_MantencionId",
                        column: x => x.MantencionId,
                        principalTable: "Mantenciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MantencionEmpleado_EmpleadoRut1",
                table: "MantencionEmpleado",
                column: "EmpleadoRut1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MantencionEmpleado");
        }
    }
}

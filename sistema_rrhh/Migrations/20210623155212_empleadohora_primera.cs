using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_rrhh.Migrations
{
    public partial class empleadohora_primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.RenameTable(
                name: "Empleado",
                newName: "Empleados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "Rut");

            migrationBuilder.CreateTable(
                name: "EmpleadosHoras",
                columns: table => new
                {
                    EmpleadoRut = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosHoras", x => new { x.EmpleadoRut, x.Fecha });
                    table.ForeignKey(
                        name: "FK_EmpleadosHoras_Empleados_EmpleadoRut",
                        column: x => x.EmpleadoRut,
                        principalTable: "Empleados",
                        principalColumn: "Rut",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadosHoras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.RenameTable(
                name: "Empleados",
                newName: "Empleado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "Rut");
        }
    }
}

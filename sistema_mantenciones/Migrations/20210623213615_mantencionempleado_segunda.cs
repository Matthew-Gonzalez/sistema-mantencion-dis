using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_mantenciones.Migrations
{
    public partial class mantencionempleado_segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MantencionEmpleado_Empleados_EmpleadoRut1",
                table: "MantencionEmpleado");

            migrationBuilder.DropForeignKey(
                name: "FK_MantencionEmpleado_Mantenciones_MantencionId",
                table: "MantencionEmpleado");

            migrationBuilder.DropForeignKey(
                name: "FK_MantencionProducto_Mantenciones_MantencionId",
                table: "MantencionProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_MantencionProducto_Productos_ProductoId",
                table: "MantencionProducto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MantencionProducto",
                table: "MantencionProducto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MantencionEmpleado",
                table: "MantencionEmpleado");

            migrationBuilder.DropIndex(
                name: "IX_MantencionEmpleado_EmpleadoRut1",
                table: "MantencionEmpleado");

            migrationBuilder.DropColumn(
                name: "EmpleadoRut1",
                table: "MantencionEmpleado");

            migrationBuilder.RenameTable(
                name: "MantencionProducto",
                newName: "MantencionesProductos");

            migrationBuilder.RenameTable(
                name: "MantencionEmpleado",
                newName: "MantencionesEmpleados");

            migrationBuilder.RenameIndex(
                name: "IX_MantencionProducto_ProductoId",
                table: "MantencionesProductos",
                newName: "IX_MantencionesProductos_ProductoId");

            migrationBuilder.AlterColumn<string>(
                name: "EmpleadoRut",
                table: "MantencionesEmpleados",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MantencionesProductos",
                table: "MantencionesProductos",
                columns: new[] { "MantencionId", "ProductoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MantencionesEmpleados",
                table: "MantencionesEmpleados",
                columns: new[] { "MantencionId", "EmpleadoRut" });

            migrationBuilder.CreateIndex(
                name: "IX_MantencionesEmpleados_EmpleadoRut",
                table: "MantencionesEmpleados",
                column: "EmpleadoRut");

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionesEmpleados_Empleados_EmpleadoRut",
                table: "MantencionesEmpleados",
                column: "EmpleadoRut",
                principalTable: "Empleados",
                principalColumn: "Rut",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionesEmpleados_Mantenciones_MantencionId",
                table: "MantencionesEmpleados",
                column: "MantencionId",
                principalTable: "Mantenciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionesProductos_Mantenciones_MantencionId",
                table: "MantencionesProductos",
                column: "MantencionId",
                principalTable: "Mantenciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionesProductos_Productos_ProductoId",
                table: "MantencionesProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MantencionesEmpleados_Empleados_EmpleadoRut",
                table: "MantencionesEmpleados");

            migrationBuilder.DropForeignKey(
                name: "FK_MantencionesEmpleados_Mantenciones_MantencionId",
                table: "MantencionesEmpleados");

            migrationBuilder.DropForeignKey(
                name: "FK_MantencionesProductos_Mantenciones_MantencionId",
                table: "MantencionesProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_MantencionesProductos_Productos_ProductoId",
                table: "MantencionesProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MantencionesProductos",
                table: "MantencionesProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MantencionesEmpleados",
                table: "MantencionesEmpleados");

            migrationBuilder.DropIndex(
                name: "IX_MantencionesEmpleados_EmpleadoRut",
                table: "MantencionesEmpleados");

            migrationBuilder.RenameTable(
                name: "MantencionesProductos",
                newName: "MantencionProducto");

            migrationBuilder.RenameTable(
                name: "MantencionesEmpleados",
                newName: "MantencionEmpleado");

            migrationBuilder.RenameIndex(
                name: "IX_MantencionesProductos_ProductoId",
                table: "MantencionProducto",
                newName: "IX_MantencionProducto_ProductoId");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoRut",
                table: "MantencionEmpleado",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmpleadoRut1",
                table: "MantencionEmpleado",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MantencionProducto",
                table: "MantencionProducto",
                columns: new[] { "MantencionId", "ProductoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MantencionEmpleado",
                table: "MantencionEmpleado",
                columns: new[] { "MantencionId", "EmpleadoRut" });

            migrationBuilder.CreateIndex(
                name: "IX_MantencionEmpleado_EmpleadoRut1",
                table: "MantencionEmpleado",
                column: "EmpleadoRut1");

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionEmpleado_Empleados_EmpleadoRut1",
                table: "MantencionEmpleado",
                column: "EmpleadoRut1",
                principalTable: "Empleados",
                principalColumn: "Rut",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionEmpleado_Mantenciones_MantencionId",
                table: "MantencionEmpleado",
                column: "MantencionId",
                principalTable: "Mantenciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionProducto_Mantenciones_MantencionId",
                table: "MantencionProducto",
                column: "MantencionId",
                principalTable: "Mantenciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MantencionProducto_Productos_ProductoId",
                table: "MantencionProducto",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

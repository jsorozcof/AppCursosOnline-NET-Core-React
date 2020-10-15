using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class AgregarColumnaFecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Instructor",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Instructor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Comentario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Comentario");

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Instructor",
                type: "nvarchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

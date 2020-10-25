using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroPublicaciones.Migrations
{
    public partial class Migracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Genero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Publicaciones",
                columns: table => new
                {
                    PublicacionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    GeneroId = table.Column<int>(nullable: false),
                    NombreCancion = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Wallpaper = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicaciones", x => x.PublicacionId);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Genero" },
                values: new object[] { 1, "Trap" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Genero" },
                values: new object[] { 2, "Dubstep" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Genero" },
                values: new object[] { 3, "House" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Genero" },
                values: new object[] { 4, "Bass" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Genero" },
                values: new object[] { 5, "Chill" });

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_GeneroId",
                table: "Publicaciones",
                column: "GeneroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}

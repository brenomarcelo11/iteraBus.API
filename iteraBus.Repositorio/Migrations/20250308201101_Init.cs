using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iteraBus.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rotas",
                columns: table => new
                {
                    RotaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotas", x => x.RotaId);
                });

            migrationBuilder.CreateTable(
                name: "Onibus",
                columns: table => new
                {
                    OnibusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Número = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Linha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onibus", x => x.OnibusId);
                    table.ForeignKey(
                        name: "FK_Onibus_Rotas_Rota",
                        column: x => x.Rota,
                        principalTable: "Rotas",
                        principalColumn: "RotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localização",
                columns: table => new
                {
                    LocalizacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnibusId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<double>(type: "float(10)", precision: 10, scale: 6, nullable: false),
                    Longitude1 = table.Column<double>(type: "float", nullable: false),
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localização", x => x.LocalizacaoId);
                    table.ForeignKey(
                        name: "FK_Localização_Onibus_OnibusId",
                        column: x => x.OnibusId,
                        principalTable: "Onibus",
                        principalColumn: "OnibusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localização_OnibusId",
                table: "Localização",
                column: "OnibusId");

            migrationBuilder.CreateIndex(
                name: "IX_Onibus_Rota",
                table: "Onibus",
                column: "Rota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localização");

            migrationBuilder.DropTable(
                name: "Onibus");

            migrationBuilder.DropTable(
                name: "Rotas");
        }
    }
}

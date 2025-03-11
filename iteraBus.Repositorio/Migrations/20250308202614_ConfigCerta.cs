using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iteraBus.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class ConfigCerta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Número",
                table: "Onibus",
                newName: "Placa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Placa",
                table: "Onibus",
                newName: "Número");
        }
    }
}

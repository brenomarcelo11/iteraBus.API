using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iteraBus.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class ConfigNova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Linha",
                table: "Onibus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Linha",
                table: "Onibus",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

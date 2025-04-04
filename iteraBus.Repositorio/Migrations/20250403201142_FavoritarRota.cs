using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iteraBus.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class FavoritarRota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioRotasFavoritas",
                columns: table => new
                {
                    RotasFavoritasId = table.Column<int>(type: "int", nullable: false),
                    UsuariosQueFavoritaramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRotasFavoritas", x => new { x.RotasFavoritasId, x.UsuariosQueFavoritaramId });
                    table.ForeignKey(
                        name: "FK_UsuarioRotasFavoritas_Rotas_RotasFavoritasId",
                        column: x => x.RotasFavoritasId,
                        principalTable: "Rotas",
                        principalColumn: "RotaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRotasFavoritas_Usuario_UsuariosQueFavoritaramId",
                        column: x => x.UsuariosQueFavoritaramId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRotasFavoritas_UsuariosQueFavoritaramId",
                table: "UsuarioRotasFavoritas",
                column: "UsuariosQueFavoritaramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioRotasFavoritas");
        }
    }
}

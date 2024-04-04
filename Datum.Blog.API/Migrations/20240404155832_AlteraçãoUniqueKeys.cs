using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datum.Blog.API.Migrations
{
    /// <inheritdoc />
    public partial class AlteraçãoUniqueKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UsuarioId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UsuarioId",
                table: "Posts",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UsuarioId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UsuarioId",
                table: "Posts",
                column: "UsuarioId",
                unique: true);
        }
    }
}

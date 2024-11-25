using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToBoardGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_board_games_ExternalId",
                table: "board_games",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_board_games_Name",
                table: "board_games",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_board_games_ExternalId",
                table: "board_games");

            migrationBuilder.DropIndex(
                name: "IX_board_games_Name",
                table: "board_games");
        }
    }
}

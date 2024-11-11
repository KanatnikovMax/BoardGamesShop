using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "users");
        }
    }
}

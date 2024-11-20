using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndexesUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_ExternalId",
                table: "users",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Login",
                table: "users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PasswordHash",
                table: "users",
                column: "PasswordHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PhoneNumber",
                table: "users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_ExternalId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_Login",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_PasswordHash",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_PhoneNumber",
                table: "users");
        }
    }
}

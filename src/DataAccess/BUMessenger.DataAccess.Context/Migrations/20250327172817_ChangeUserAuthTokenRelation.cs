using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUMessenger.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAuthTokenRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens",
                column: "UserId",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class secondtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ChatModelId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d7fc4ba6-4957-41a7-96b5-52b65c06bc35", 0, null, "e80d8f11-941c-47a1-b309-98dc07b850c2", "Admin@Mail.com", true, false, null, "ADMIN@MAIL.COM", "admin", "AQAAAAEAACcQAAAAELcLOVTnATJisaRwyNjZM03knPRJttCR8KSEIfbMn54vOzD8gebjyaDER5UF3vtekg==", null, false, "42031d20-cd9f-4cc1-a42c-6fd46b53e1d3", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35");
        }
    }
}

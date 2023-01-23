using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class seededdatabasewithsomedummydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "ChatCreated", "ChatEnded", "CreatorId", "IsChatEdited", "IsChatEncrypted", "IsChatEnded", "Name", "UserId" },
                values: new object[] { 1, new DateTime(2023, 1, 23, 15, 19, 23, 736, DateTimeKind.Local).AddTicks(1061), null, "d7fc4ba6-4957-41a7-96b5-52b65c06bc35", false, false, false, "CoolChat", "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a57f878-35a4-4be9-8c00-c21eafc4c3f7", "AQAAAAEAACcQAAAAEH6b/PrbiDU0k1E5f1BrYKIqlxiUKCH+k8D6QjqN/+IdROInDWlrHfxvD7pcVILgUQ==", "ff77f152-ecb9-4bb3-a76d-1625c3f716f4" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "IsMessageDeleted", "IsMessageEdited", "IsMessageEncrypted", "Message", "MessageCreated", "MessageDeleted", "MessageEdited", "UserId" },
                values: new object[] { 1, 1, false, false, false, "This is just a test message for the api's glhf", new DateTime(2023, 1, 23, 15, 19, 23, 736, DateTimeKind.Local).AddTicks(1098), null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e80d8f11-941c-47a1-b309-98dc07b850c2", "AQAAAAEAACcQAAAAELcLOVTnATJisaRwyNjZM03knPRJttCR8KSEIfbMn54vOzD8gebjyaDER5UF3vtekg==", "42031d20-cd9f-4cc1-a42c-6fd46b53e1d3" });
        }
    }
}

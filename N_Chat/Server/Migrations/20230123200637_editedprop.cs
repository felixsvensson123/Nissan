using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class editedprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 21, 6, 36, 837, DateTimeKind.Local).AddTicks(7691));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 21, 6, 36, 837, DateTimeKind.Local).AddTicks(7769));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f39d5b86-11ef-4e3f-8249-af4eb7541d97", "AQAAAAEAACcQAAAAECeVhDF/tiaH3PN/sV93UJMzeHaFM5yXW5YXbJvcmuKsQehQ+bSTKVUupizK0F2K2g==", "e95e5535-38d6-45b9-9079-8845d634cc53" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 19, 40, 3, 699, DateTimeKind.Local).AddTicks(7719));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 19, 40, 3, 699, DateTimeKind.Local).AddTicks(7754));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "416d227b-a013-4430-b76c-95dab6b9d40f", "AQAAAAEAACcQAAAAENpssC50smrKfdcf9vbsvmmK09V+RAoK3FYw4tHKOud9POrmLck6u6J7O5u3JO7Q2w==", "36695f1f-b30d-437a-9bb0-94548bf004a2" });
        }
    }
}

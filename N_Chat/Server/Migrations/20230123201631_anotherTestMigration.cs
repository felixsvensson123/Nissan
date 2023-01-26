using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class anotherTestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 21, 16, 31, 546, DateTimeKind.Local).AddTicks(8475));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 21, 16, 31, 546, DateTimeKind.Local).AddTicks(8505));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "748d4719-a8ec-4bc2-8ad5-d26a28256844", "AQAAAAEAACcQAAAAEMKjKutjzhh8wq3EvQjubHOYpFIIzkytXH+f16VChk9I7GcHPgL1lq7xYZiSVYOCmQ==", "793eb7f4-d2d4-4740-b63f-5bb4b1fbcd23" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 21, 13, 55, 521, DateTimeKind.Local).AddTicks(3813));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 21, 13, 55, 521, DateTimeKind.Local).AddTicks(3844));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edd2ce47-a9e6-4782-b5b6-da8054bb091c", "AQAAAAEAACcQAAAAELixjWDTrmY2YxwxnPb/VGhPh3mnsPKsMkjKUxc82FFWVUcjzgdEkE/XhDuGvzvB4g==", "a48265b6-4180-4eac-91ee-2d569b626908" });
        }
    }
}

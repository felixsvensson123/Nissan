using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 15, 25, 18, 134, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 15, 25, 18, 134, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a6f8134-faae-43b6-a76c-b059f004707b", "AQAAAAEAACcQAAAAELMD+1dgxgCo7NpXj1+aJkMRDe5nhVbu4z1rxVD4nmN3Phmh6tXKV0OGS1G1MKWv1Q==", "373ae79f-721f-4d30-9716-56c5a22a50b3" });
        }
    }
}

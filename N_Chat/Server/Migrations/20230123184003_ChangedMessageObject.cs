using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedMessageObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "MessageId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Messages",
                newName: "Id");

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

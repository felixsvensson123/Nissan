using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class changedModelBuilderEntityMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 15, 19, 23, 736, DateTimeKind.Local).AddTicks(1061));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 15, 19, 23, 736, DateTimeKind.Local).AddTicks(1098));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a57f878-35a4-4be9-8c00-c21eafc4c3f7", "AQAAAAEAACcQAAAAEH6b/PrbiDU0k1E5f1BrYKIqlxiUKCH+k8D6QjqN/+IdROInDWlrHfxvD7pcVILgUQ==", "ff77f152-ecb9-4bb3-a76d-1625c3f716f4" });
        }
    }
}

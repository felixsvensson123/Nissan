using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class changedmodelbuilderaddedmembeRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 25, 11, 25, 49, 443, DateTimeKind.Local).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 25, 11, 25, 49, 443, DateTimeKind.Local).AddTicks(6484));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d153c726-e709-4946-824b-0ed63bbf136a", "fe9bbcec-13ad-461c-951a-26c8d286fcd6", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae704e16-edca-489f-ba06-a566c777eb03", "AQAAAAEAACcQAAAAENtXMuYiRRyNq5AgUdSFo11zBH8/7O88YwXaS+CB6lz442sWXmjJ7cZQYojmxUppVg==", "c7f1c09f-1c54-43b2-bf6a-35df018d6164" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d153c726-e709-4946-824b-0ed63bbf136a");

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class changedIdentityRoleMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 25, 11, 29, 21, 177, DateTimeKind.Local).AddTicks(7662));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 25, 11, 29, 21, 177, DateTimeKind.Local).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d153c726-e709-4946-824b-0ed63bbf136a",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8354804-2728-4084-a122-b741d4408a72", "Member", "MEMBER" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e809653-c238-4f30-a08a-f23cdd50e13a", "AQAAAAEAACcQAAAAEG5JMplprqoWRfiiG73/2MDlaPhPDsadxDWSVcsD4TjhlJLcGOMo6GdN0yqSCo8ecA==", "3c8f8fdb-e4c9-4c23-8fc7-b7fe4ea46b09" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d153c726-e709-4946-824b-0ed63bbf136a",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe9bbcec-13ad-461c-951a-26c8d286fcd6", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae704e16-edca-489f-ba06-a566c777eb03", "AQAAAAEAACcQAAAAENtXMuYiRRyNq5AgUdSFo11zBH8/7O88YwXaS+CB6lz442sWXmjJ7cZQYojmxUppVg==", "c7f1c09f-1c54-43b2-bf6a-35df018d6164" });
        }
    }
}

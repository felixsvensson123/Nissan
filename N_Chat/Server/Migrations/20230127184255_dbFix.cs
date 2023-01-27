using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class dbFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsChatEnded",
                table: "Chats",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 27, 19, 42, 55, 278, DateTimeKind.Local).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 27, 19, 42, 55, 278, DateTimeKind.Local).AddTicks(9284));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 28, 0, 42, 55, 278, DateTimeKind.Local).AddTicks(9287));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 27, 19, 42, 55, 278, DateTimeKind.Local).AddTicks(9289));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 27, 19, 42, 55, 278, DateTimeKind.Local).AddTicks(9290));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d153c726-e709-4946-824b-0ed63bbf136a",
                column: "ConcurrencyStamp",
                value: "88729e40-139d-4147-a453-2af2cbc5ca55");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b95b96dd-857a-4e61-ad8b-df8f8433dcc8", "AQAAAAEAACcQAAAAEAk3sro2xcMEViWhDZUECWPoHo3LxVQenaeeHzDpQIFvRBasUdItlMza3iZ01DKoUw==", "049d0078-84e2-4cd6-84a8-ee172fbd7a22" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e20e142-36de-4027-97df-262b19316bfb", "AQAAAAEAACcQAAAAEL/XJceDzNvYLgsieR9qZLKFI9uABxKaBMvzHlUjSI6vMhplbwUPFr81MTXBOSJ5Hg==", "31ba0e46-6f1f-4b2f-99df-96acb8d49ac7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsChatEnded",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 26, 16, 47, 25, 784, DateTimeKind.Local).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 26, 16, 47, 25, 784, DateTimeKind.Local).AddTicks(6786));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 26, 21, 47, 25, 784, DateTimeKind.Local).AddTicks(6790));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 26, 16, 47, 25, 784, DateTimeKind.Local).AddTicks(6792));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 26, 16, 47, 25, 784, DateTimeKind.Local).AddTicks(6794));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d153c726-e709-4946-824b-0ed63bbf136a",
                column: "ConcurrencyStamp",
                value: "8a0acbf7-8533-4b12-8b6c-c18a384c77e8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35991a8a-4e90-42c5-8e5d-b4b5793645f0", "AQAAAAEAACcQAAAAEDSnrJtubOKtWh+AOpdaqDa7Iy+XWbqJ2StI3PmGKiXCy6eE7bbrywzxPcW/5B8aqA==", "182c5fc9-a9c6-4a2d-b9a1-df5d5264009f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcf28a6b-12b1-42c0-a1a9-d4557b638288", "AQAAAAEAACcQAAAAEKgColkytHwAZ28sQEfvP93JyG1VMoV3WU+7WRJjgK3bGmd8AVkGmk8ktyqSFMee/g==", "9afd0b68-e435-4e48-ba16-c9239131ebe0" });
        }
    }
}

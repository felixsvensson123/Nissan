using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class addedUserRolesToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2339));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 31, 0, 43, 0, 574, DateTimeKind.Local).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2391));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d153c726-e709-4946-824b-0ed63bbf136a",
                column: "ConcurrencyStamp",
                value: "5874cbc3-9e32-424c-8169-4fbdcbd670bc");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1678ba6-7957-21a7-96b5-12b64c06bc25", "0ab63f60-f120-4c6c-98ab-39d16b8b65b5", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "d7fc4ba6-4957-41a7-96b5-52b65c06bc35", "d1678ba6-7957-21a7-96b5-12b64c06bc25" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "ecf82a42-7fe1-46f5-b051-48653093fc0f", "Css@live.se", "css@live.se", "felix", "AQAAAAEAACcQAAAAEEW7iNWCLBX2xlKz0m7vgZHevCfrtY+fMv52xROJ9gAnFmwmmksuGR+4+vyrrybImA==", "a8108c51-f6e9-4a23-b2dd-1ba8abee6d4e", "felix" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d03c5159-3441-4e22-837d-a1684a642b7c", "adminuser@gmail.com", "adminuser@gmail.com", "AQAAAAEAACcQAAAAEE8Chgs/SB3JdliyyMLufSyNeqwCTAQQjgxAcbNiFSszba54K20ccKAkyvTVOLN+tQ==", "08c39a92-c3ab-4b92-8ef7-51efab503871" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UserId",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b95b96dd-857a-4e61-ad8b-df8f8433dcc8", "Admin@Mail.com", "ADMIN@MAIL.COM", "admin", "AQAAAAEAACcQAAAAEAk3sro2xcMEViWhDZUECWPoHo3LxVQenaeeHzDpQIFvRBasUdItlMza3iZ01DKoUw==", "049d0078-84e2-4cd6-84a8-ee172fbd7a22", "admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e20e142-36de-4027-97df-262b19316bfb", "Admin@Mail.com", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEL/XJceDzNvYLgsieR9qZLKFI9uABxKaBMvzHlUjSI6vMhplbwUPFr81MTXBOSJ5Hg==", "31ba0e46-6f1f-4b2f-99df-96acb8d49ac7" });
        }
    }
}

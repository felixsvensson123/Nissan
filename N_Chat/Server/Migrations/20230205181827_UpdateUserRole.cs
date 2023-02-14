using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25");

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
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChatCreated",
                value: new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1368));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 6, 0, 18, 27, 390, DateTimeKind.Local).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "57482375-9769-410d-b7d7-d77d6263f728");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "d15c96d6-39b6-4290-a33c-b090d84bfd1c");

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1678ba6-7957-21a7-96b5-12b64c06bc25", "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d562898f-a346-4f56-aedd-9104dedd0a71", "AQAAAAEAACcQAAAAEErKxbwKZmz/XeUE2BFvBBi/iZzf+IPyVeOSh+fpYolmUh2/mUNKoR/2HKOhHv1DFA==", "611efa93-ac10-451c-b3fd-bf8b76e3220a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1366f341-0279-4ce1-9dab-584d9f6e0f90", "AQAAAAEAACcQAAAAEFIir5Y9Cj2b1aLcq2VPFkI44XyJzILHGt4Yig+hjw23r4IJ4/9OvFwUKV5wsaKXAw==", "1555d260-0b99-449a-958c-3d1ac5abbaa4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1678ba6-7957-21a7-96b5-12b64c06bc25", "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" });

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChatCreated",
                value: new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(884));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(915));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 23, 34, 28, 289, DateTimeKind.Local).AddTicks(917));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "521bef94-a782-41ba-895b-10959b9f1172");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "45f60bf3-6610-4769-aa14-db8d69a60376");

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1678ba6-7957-21a7-96b5-12b64c06bc25", "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddee5992-debf-4a74-925b-6a4b0212afd6", "AQAAAAEAACcQAAAAEC7WMleFTu6blEnrKnV1LfcubMEZfV5Kfi18hS77rhUtzWPqOOiLQJiBqx6vdEQdIQ==", "1d055198-a0d1-41a9-af00-04ad50e7b771" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f92a9244-e885-47d9-824c-db862cc8cc21", "AQAAAAEAACcQAAAAEPFJ5gjXgLK6QJnLFkLYZ+dpdBi69W5sk4KkHdspU1WcMVuGqEx4FL3Cq3NUpX5feA==", "d8d76392-3e1a-4ccf-8152-05c9239c61e3" });
        }
    }
}

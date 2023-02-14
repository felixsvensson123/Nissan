using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatModelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Connected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                });

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ChatModelId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "ddee5992-debf-4a74-925b-6a4b0212afd6", "AQAAAAEAACcQAAAAEC7WMleFTu6blEnrKnV1LfcubMEZfV5Kfi18hS77rhUtzWPqOOiLQJiBqx6vdEQdIQ==", "1d055198-a0d1-41a9-af00-04ad50e7b771" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ChatModelId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "f92a9244-e885-47d9-824c-db862cc8cc21", "AQAAAAEAACcQAAAAEPFJ5gjXgLK6QJnLFkLYZ+dpdBi69W5sk4KkHdspU1WcMVuGqEx4FL3Cq3NUpX5feA==", "d8d76392-3e1a-4ccf-8152-05c9239c61e3" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatModelId",
                table: "Users",
                column: "ChatModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatModelId",
                table: "Users",
                column: "ChatModelId",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatModelId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatModelId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChatCreated",
                value: new DateTime(2023, 2, 4, 10, 48, 59, 850, DateTimeKind.Local).AddTicks(3752));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 10, 48, 59, 850, DateTimeKind.Local).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 15, 48, 59, 850, DateTimeKind.Local).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 10, 48, 59, 850, DateTimeKind.Local).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "MessageCreated",
                value: new DateTime(2023, 2, 4, 10, 48, 59, 850, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "ef5ebfff-07ca-42ce-bc82-d79176141785");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "ff2e4058-5729-4ac1-89e5-66db1b28b543");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e891dec1-b60b-43b9-b543-d645b4acfc62", "AQAAAAEAACcQAAAAEATZR5vE9AA/3ar1CaGMqDASm8btP8ZaQPalrBUMaLr9ZEkkmbVRoO/tcId5OTTzcQ==", "fa2f380f-eda5-4dba-967a-fe1a41cff33c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e18ae57-a6a8-428d-99ad-fbece5eaffdf", "AQAAAAEAACcQAAAAEASTMOf7+cHBLJVaCI/Fbm/yVPRK6Pp77VRQg2sSj4is7nAoS8Rjkkr27RxSgpRmxA==", "f8c767fd-185f-4879-94d3-e88be9ae7e42" });
        }
    }
}

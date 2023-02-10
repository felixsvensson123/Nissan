using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class changedPkForChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chats",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                newName: "IX_Chats_UserName");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "e0dba9cb-103b-4958-9699-652fae3646d4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "84579cc5-d384-4672-a383-d9488393e6fa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc3762ba-9481-44c3-a2a8-fc520ebd8119", "AQAAAAEAACcQAAAAEC1AG345F6uDAAfeVOWtA4qvyUgNyUHaXuHHEHyEfOg7oP6MGiHBBfDYUduEC3qOug==", "e72a4ad3-f55c-4c78-9820-3956874f336c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de3eee2e-030a-4810-9d23-0fb35b319405", "AQAAAAEAACcQAAAAEAKk2rY6IAUhxdyP09tCTzUrV17O4bGzK31iOd0e75JFvot6GM57Qo15eXNSJCJohA==", "9b62c7c4-772b-40e9-8b3f-6e9c5372ff71" });

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserName",
                table: "Chats",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserName",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Chats",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserName",
                table: "Chats",
                newName: "IX_Chats_UserId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "c9884d83-76d4-4bd3-895e-be3dd2d5da29");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "1bfc8557-2b77-4e3e-becf-cd712005a9eb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca7cb98a-8b29-4473-8614-6c059f124c7a", "AQAAAAEAACcQAAAAEN2WhG21bAqw+G1ysZNNGrXt/KxMH/W09IC9S0atao4GAR7yxANy+4LODm+KGJbtxg==", "ee48205b-c900-4945-9626-7cf57285b022" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d919fa53-fac7-4d1a-827c-7556365f55bf", "AQAAAAEAACcQAAAAELfooTiytAJMB9WOJXk1JbJQZp5qDtqOfecBuV7isLr/NvURiXNKehGSQWZeVYg1tw==", "6eb64cb1-7b22-4a59-8a49-f3c548711351" });

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

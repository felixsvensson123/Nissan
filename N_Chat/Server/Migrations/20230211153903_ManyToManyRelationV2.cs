using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyRelationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => new { x.UserId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_UserChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "0ff3ad2b-9a8f-488d-965e-ec0f9948042e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "62a58d6d-4031-4201-bf85-5367c94f0336");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "162f77d0-b365-484f-b196-8aa2be8dcd66", "AQAAAAEAACcQAAAAEN+FQlLRus9IQZ3d+iJyLKqEfOEoE2uEHFFu8uYfwgGLO+xIJidQiNNXcAlWzYr7CQ==", "b4197904-dcf6-417b-bd96-a055a9999228" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "280f8851-bb15-41fc-a1d7-f176fbd1bedc", "AQAAAAEAACcQAAAAELSDqbsUg3gSTqfwQ8FkbN+ESWyqLrXuQ979JV2dy1hDz4e5O+3ocj/13w50ewYVQA==", "6d4867a7-5cc2-42f6-9da2-12fca11f87f3" });

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ChatId",
                table: "UserChats",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.AddColumn<int>(
                name: "ChatModelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "474efe24-8364-4121-ba35-929e4e859114");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "4d1513e2-ddf3-4707-bf69-307b2926e820");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ChatModelId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "11312ee2-e80c-43dc-a47c-7ec149e695f2", "AQAAAAEAACcQAAAAEByfY6TmhP41IFdH52gCrrKrkoHrHRVRxZTsrsAYWzHopl5YYgbLKv89CmzD5DR2uQ==", "34db2a82-5ae4-4ba9-976d-3badc472e93e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ChatModelId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "ca5e12d0-4df7-44a7-9ff7-c1c1b8d0b2eb", "AQAAAAEAACcQAAAAECvuzGN0KhZ2n+o/qlnUFjLZ4KXlILGVPTTDUGDuZzOPcnL5gQC/0oZkUeBEca4Ybg==", "55514883-d153-4ae1-a474-221dbd0c0ca9" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatModelId",
                table: "Users",
                column: "ChatModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatModelId",
                table: "Users",
                column: "ChatModelId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}

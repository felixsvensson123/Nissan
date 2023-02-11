using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedShowDetailsBoolForChats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowDetails",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "ce34cd55-ea63-4592-a58e-077a17740f57");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "6450d634-08cf-4a4c-8e76-21a1f1a2236b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a659839-39ab-43c2-9fa6-c725f742aa65", "AQAAAAEAACcQAAAAEG/6bIU0kMZrHMx/gAW8/qlTX+jibM8HmyI1CirySIgnyCGwEdvy0Q22siM4yn87dA==", "40f0754c-e2cd-4ff3-a1ef-563226e6bc8f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "101f1ab2-2688-4406-9415-c86926ed3842", "AQAAAAEAACcQAAAAENej//2hXHxhM451I/YAHxC1yH+5AMdyBXD6ebewfluWIIAHkU/M465se7PrHrx4mA==", "86e19c10-84e5-4c61-8a4c-3c6067ab1cdb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowDetails",
                table: "Chats");

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
        }
    }
}

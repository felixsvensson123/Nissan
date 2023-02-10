using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class setkeyforusermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 5);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "ChatCreated", "ChatEnded", "CreatorId", "IsChatEdited", "IsChatEncrypted", "IsChatEnded", "Name", "UserId" },
                values: new object[] { 5, new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1368), null, "d7fc4ba6-4957-41a7-96b5-52b65c06bc35", false, false, false, "CoolChat", "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" });

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

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "IsMessageDeleted", "IsMessageEdited", "IsMessageEncrypted", "Message", "MessageCreated", "MessageDeleted", "MessageEdited", "UserId" },
                values: new object[,]
                {
                    { 2, 5, false, false, false, "This one admin message 1", new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1406), null, null, "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" },
                    { 3, 5, false, false, false, "This one admin message 2", new DateTime(2023, 2, 6, 0, 18, 27, 390, DateTimeKind.Local).AddTicks(1409), null, null, "d7fc4ba6-4957-41a7-96b5-52b65c06bc35" },
                    { 4, 5, false, false, false, "This is felix message 1", new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1411), null, null, "ded90182-7b04-41e0-aef6-8977a4d1c292" },
                    { 5, 5, false, false, false, "This is just a test message for the api's glhf", new DateTime(2023, 2, 5, 19, 18, 27, 390, DateTimeKind.Local).AddTicks(1413), null, null, "ded90182-7b04-41e0-aef6-8977a4d1c292" }
                });
        }
    }
}

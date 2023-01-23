﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 21, 8, 24, 841, DateTimeKind.Local).AddTicks(1420));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 21, 8, 24, 841, DateTimeKind.Local).AddTicks(1457));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b64efa80-06ab-4e15-a3f2-b9ddb0d7e0d3", "AQAAAAEAACcQAAAAEJD/gtA2uRcPwd2omQk5TP22QrFjuEmELTwQIBXrQnopeNRQAMSJpHudzf+kHHXXHg==", "cde462bf-fa4b-422b-be3d-02c99759aec1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatCreated",
                value: new DateTime(2023, 1, 23, 19, 40, 3, 699, DateTimeKind.Local).AddTicks(7719));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "MessageCreated",
                value: new DateTime(2023, 1, 23, 19, 40, 3, 699, DateTimeKind.Local).AddTicks(7754));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "416d227b-a013-4430-b76c-95dab6b9d40f", "AQAAAAEAACcQAAAAENpssC50smrKfdcf9vbsvmmK09V+RAoK3FYw4tHKOud9POrmLck6u6J7O5u3JO7Q2w==", "36695f1f-b30d-437a-9bb0-94548bf004a2" });
        }
    }
}

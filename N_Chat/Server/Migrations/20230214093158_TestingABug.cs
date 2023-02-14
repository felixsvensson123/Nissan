using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N_Chat.Server.Migrations
{
    public partial class TestingABug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "MessageCreated",
                table: "Messages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "d1b639b2-53af-41f2-bb63-1711672b612e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "5003ad62-a713-447a-862d-4502023080fd");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "522fd56a-05b5-41fb-9967-7dcd9ff8bff1", "AQAAAAEAACcQAAAAEKmjO05AIJD/ZgVV6AHyH23r1c/EE0JajU36gikhRGEAR861OAM0k2L6TL3mPiXBIQ==", "8507ddb6-7661-404d-8b6c-86e4a14aa02a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17310562-1ce3-446d-8e9b-6d2714ec9f4d", "AQAAAAEAACcQAAAAEGJlVFsb3nATfUgNWwzJmMwfY8e32iO/nLx+DE2H0zPTvV4NRp2cdTCzIR4+OOpNaw==", "a4245a7a-8b22-4d4d-8905-41b8c6146500" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "MessageCreated",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "0cbcba21-9538-43f8-bc20-ed777a99a8f2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "ef041358-3e2d-438f-99be-d3299e532b41");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44dce2d8-7297-4c6c-a729-1ab8b1575d1e", "AQAAAAEAACcQAAAAEL1mmijZxAuqiKa+st+/0kW41LI9Zx4u2WQaB4K8XCYprePwGJEaa23ScWPQwrd4+g==", "3e8fd506-cd64-4743-8241-c9cb515debc4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88e7fed6-2551-4abe-9271-33526c6df73b", "AQAAAAEAACcQAAAAEAujpaVHYHV1sJUMdqLvZ2oZDu2CyS0s1Io16DAwpJx+oyYP2H+3cvNiZPzEhUviYg==", "2f38eda8-84e4-4b12-9c94-d6ee48ab81b9" });
        }
    }
}

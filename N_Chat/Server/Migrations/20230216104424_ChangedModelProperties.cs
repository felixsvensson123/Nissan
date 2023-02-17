using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N_Chat.Server.Migrations
{
    public partial class ChangedModelProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MessageCreated",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowDetails",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "9a9fa430-ba0d-48e5-9aee-bc33988338a0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "efabc1d9-074b-42df-877e-4ff84db0286f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "225dcbde-67cb-4853-ad6f-65e0b1ea2086", "AQAAAAEAACcQAAAAEH7ZKtYLDTAjQjRhV6pf6ShKZqU9jliKdHGBrvpiWUTRU/y6giadGnrdVhaN9hSbuw==", "4b47797f-e4f0-4662-9e07-d86e2145a7ae" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91bb0d30-78ab-4cc9-8834-f594f4850d04", "AQAAAAEAACcQAAAAEAchBC6NQ4DkK5sRuoVFsdIlG2EM/LoGcarEBOe2rinsF9QDDFhBJ/j9lDKb4t1elg==", "df9309e0-8dcb-4a73-b0eb-d1525b027e97" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShowDetails",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Messages");

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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NChat.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedModelListsToICollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11312ee2-e80c-43dc-a47c-7ec149e695f2", "AQAAAAEAACcQAAAAEByfY6TmhP41IFdH52gCrrKrkoHrHRVRxZTsrsAYWzHopl5YYgbLKv89CmzD5DR2uQ==", "34db2a82-5ae4-4ba9-976d-3badc472e93e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca5e12d0-4df7-44a7-9ff7-c1c1b8d0b2eb", "AQAAAAEAACcQAAAAECvuzGN0KhZ2n+o/qlnUFjLZ4KXlILGVPTTDUGDuZzOPcnL5gQC/0oZkUeBEca4Ybg==", "55514883-d153-4ae1-a474-221dbd0c0ca9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "ac0d9673-b2fb-4f66-8ec9-fc990de485ae");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "b7aff65a-7ef1-4f8c-9fc7-8ba11b938b31");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11500ebb-89f7-4c8f-a6dd-b370cd889bb2", "AQAAAAEAACcQAAAAEGU9DYnFMDfllh95TFDUQjIJL9la6pmuYqMSKseKo1LsnN4W9h6f5QpEAj4Bay4IAQ==", "d91baa38-e951-4950-99c5-75112540c41a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c9892d3-713f-46cb-af57-b58ba70fe69c", "AQAAAAEAACcQAAAAEGDu4z4wh/ZAw1e8g5Oqzh/23esTPaA8CAb7bFvFI/WAC4uDfbySUWrqOUm20pgRHA==", "6f698773-5047-43b2-826a-ee490afd614b" });
        }
    }
}

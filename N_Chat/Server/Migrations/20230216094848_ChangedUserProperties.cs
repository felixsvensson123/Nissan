using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N_Chat.Server.Migrations
{
    public partial class ChangedUserProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "405cdd18-7b31-4f1f-bfa0-a3481bafa687");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "04c8ccd9-7fb1-4ade-8250-1e6084636150");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51df1116-be20-44a7-9696-252b1812122b", "AQAAAAEAACcQAAAAEGKBvxfcF3vzMhrPpPzWz4yFkRHBkHpGT7VZ/+CL29mz4R3PWGTlxHzwksdMcoOv6A==", "11f5ff12-4203-41a5-8b86-4d747cdf4735" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fe7d88e-1823-4120-b366-67b87d7daf5b", "AQAAAAEAACcQAAAAEH/6hPdX5d82uDOTTh8qG5+hr5b6mqcnwtszzwF7mAK2aLDJAc+B2MspMpczGiki9A==", "747d94b5-7e3d-4442-89e6-11cc0dc5ed0e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Users");

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

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N_Chat.Server.Migrations
{
    public partial class removedUserNameString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                column: "ConcurrencyStamp",
                value: "07c3c2d6-b711-4a02-9ae1-d58d28000cf9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                column: "ConcurrencyStamp",
                value: "8462db72-24c9-4647-8e8a-6cb00b9cd748");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21ec92ae-4e01-44bb-b217-0cac26bb1e89", "AQAAAAEAACcQAAAAEFqkf5akrRin2IwDYoEfwJa8obyWVpOSKwXFg4+WQC/fKwatoLDDnmBjqgS3G1SAMQ==", "d4e4c343-e6e1-436f-ad48-eaf849e07fe3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ded90182-7b04-41e0-aef6-8977a4d1c292",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28c7adfc-b4ef-4430-ac10-b46259c151ef", "AQAAAAEAACcQAAAAECoG3YO+6lfmW4Kr7F8nT4sPkArj0PjyaU6TELN+91wYwF3QBvHlx1L76IUUD3//Kg==", "e0d65ae3-1275-48c0-b22b-acbc06918f11" });
        }
    }
}

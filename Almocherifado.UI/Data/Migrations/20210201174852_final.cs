using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a097a1d-f706-46ba-9501-02424f7850ee");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1fd37580-22c1-493a-8833-422dab72e0f5", 0, "6c9f73ad-c42c-42a1-908b-4a549dff1a74", "user@admin", true, false, null, "user@admin", "admin@admin", "AQAAAAEAACcQAAAAENTQJcpPJkcuMCNSmM16XZgyM4MDwGVXIliqK0NcTHNPYqUZRxTyoJto3taG9FpVag==", null, false, "", false, "admin@admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1fd37580-22c1-493a-8833-422dab72e0f5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3a097a1d-f706-46ba-9501-02424f7850ee", 0, "b33c7fd5-756d-487e-be93-2fc7e15c7867", "user@admin", true, false, null, "user@admin", "admin@admin", "AQAAAAEAACcQAAAAEHJ599YQtzBXZIMfApIDcBtptJZTGTZhBbIrWpxoXLjDs9Akk/aa6l7qxorEcbHO1Q==", null, false, "", false, "admin@admin" });
        }
    }
}

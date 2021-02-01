using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7743f953-c429-46cd-bd48-aca09cbfdfeb");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3a097a1d-f706-46ba-9501-02424f7850ee", 0, "b33c7fd5-756d-487e-be93-2fc7e15c7867", "user@admin", true, false, null, "user@admin", "admin@admin", "AQAAAAEAACcQAAAAEHJ599YQtzBXZIMfApIDcBtptJZTGTZhBbIrWpxoXLjDs9Akk/aa6l7qxorEcbHO1Q==", null, false, "", false, "admin@admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a097a1d-f706-46ba-9501-02424f7850ee");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7743f953-c429-46cd-bd48-aca09cbfdfeb", 0, "80e26e4e-3823-40a3-bedd-3e6961ec6cba", "user@admin", true, false, null, "user@admin", "admin", "AQAAAAEAACcQAAAAEJobj+MofYovQbKZz34F4rU6gRmfQptvi+o+98flL2GTRy32TDtEkFhWZy8+tF8DgA==", null, false, "", false, "admin" });
        }
    }
}

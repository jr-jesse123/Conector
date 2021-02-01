using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ffa964e-cbd2-4b5e-92a1-ce56aface168");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6ffa964e-cbd2-4b5e-92a1-ce56aface168", 0, "aa817e78-1809-45c3-9b0f-7b859822da5e", "admin@admin", true, false, null, null, null, "AQAAAAEAACcQAAAAEAFk4TCka9QD1WiD5Xva2fA1MGfeJPVZpR2pLcryB4N3r6k/bEJnclJEblSwqWZV/g==", null, false, "848a9866-7af7-494f-9dbf-8a128d73f303", false, "Admin" });
        }
    }
}

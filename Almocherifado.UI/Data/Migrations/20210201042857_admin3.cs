using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e65687a-d25c-441e-87fd-d4b898403b51");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6ffa964e-cbd2-4b5e-92a1-ce56aface168", 0, "aa817e78-1809-45c3-9b0f-7b859822da5e", "admin@admin", true, false, null, null, null, "AQAAAAEAACcQAAAAEAFk4TCka9QD1WiD5Xva2fA1MGfeJPVZpR2pLcryB4N3r6k/bEJnclJEblSwqWZV/g==", null, false, "848a9866-7af7-494f-9dbf-8a128d73f303", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ffa964e-cbd2-4b5e-92a1-ce56aface168");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9e65687a-d25c-441e-87fd-d4b898403b51", 0, "ebe41466-679d-4aed-915a-16d592b8662c", "admin@admin", false, false, null, null, null, "AQAAAAEAACcQAAAAED/7vX3wybMj9VctVLUIvFg8S2JHzCqvNg6oCBZZL9/LvlkeoeqQ5Ooa6ou3CuqRCg==", null, false, "76db3325-394b-4007-a8b2-a0e0d7ada9ef", false, "Admin" });
        }
    }
}

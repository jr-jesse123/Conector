using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6ba9a7f6-9020-46d4-ac7d-4193b19959f1", 0, "be192c87-f6c7-462e-b86c-3e5fc28e68ce", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEAZT92tDdaD8Kr9iuObp2rn/JPmQvP3ZmLNui5k3Lftt0Qo1hdlX7OrRcVSIlAnbLg==", null, false, "f7ae5718-9564-48d1-a25f-a0aadb3cf59a", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ba9a7f6-9020-46d4-ac7d-4193b19959f1");
        }
    }
}

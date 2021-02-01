using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ba9a7f6-9020-46d4-ac7d-4193b19959f1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9e65687a-d25c-441e-87fd-d4b898403b51", 0, "ebe41466-679d-4aed-915a-16d592b8662c", "admin@admin", false, false, null, null, null, "AQAAAAEAACcQAAAAED/7vX3wybMj9VctVLUIvFg8S2JHzCqvNg6oCBZZL9/LvlkeoeqQ5Ooa6ou3CuqRCg==", null, false, "76db3325-394b-4007-a8b2-a0e0d7ada9ef", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e65687a-d25c-441e-87fd-d4b898403b51");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6ba9a7f6-9020-46d4-ac7d-4193b19959f1", 0, "be192c87-f6c7-462e-b86c-3e5fc28e68ce", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEAZT92tDdaD8Kr9iuObp2rn/JPmQvP3ZmLNui5k3Lftt0Qo1hdlX7OrRcVSIlAnbLg==", null, false, "f7ae5718-9564-48d1-a25f-a0aadb3cf59a", false, "Admin" });
        }
    }
}

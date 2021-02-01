using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "355d64f0-d9e6-47d0-af38-a3d9a4b35432");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7743f953-c429-46cd-bd48-aca09cbfdfeb", 0, "80e26e4e-3823-40a3-bedd-3e6961ec6cba", "user@admin", true, false, null, "user@admin", "admin", "AQAAAAEAACcQAAAAEJobj+MofYovQbKZz34F4rU6gRmfQptvi+o+98flL2GTRy32TDtEkFhWZy8+tF8DgA==", null, false, "", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7743f953-c429-46cd-bd48-aca09cbfdfeb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "355d64f0-d9e6-47d0-af38-a3d9a4b35432", "6c3bb9b4-c2a1-4430-814f-19d9e2ab2003", "Admin", "ADMIN" });
        }
    }
}

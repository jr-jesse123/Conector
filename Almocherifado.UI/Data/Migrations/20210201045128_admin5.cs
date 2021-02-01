using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.UI.Data.Migrations
{
    public partial class admin5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "355d64f0-d9e6-47d0-af38-a3d9a4b35432", "6c3bb9b4-c2a1-4430-814f-19d9e2ab2003", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "355d64f0-d9e6-47d0-af38-a3d9a4b35432");
        }
    }
}

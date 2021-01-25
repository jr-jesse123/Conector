using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.InfraEstrutura.Migrations
{
    public partial class marca_modelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Ferramentas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Ferramentas",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Ferramentas");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Ferramentas");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraEstrutura.Migrations
{
    public partial class funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmManutencao",
                table: "Ferramentas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmManutencao",
                table: "Ferramentas");
        }
    }
}

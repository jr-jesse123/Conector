using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraEstrutura.Migrations
{
    public partial class patrimonio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ferramentas",
                table: "Ferramentas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ferramentas");

            migrationBuilder.AlterColumn<string>(
                name: "Patrimonio",
                table: "Ferramentas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ferramentas",
                table: "Ferramentas",
                column: "Patrimonio");

            migrationBuilder.CreateTable(
                name: "Devolucao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FerramentaPatrimonio = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlocacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devolucao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devolucao_Alocaoes_AlocacaoId",
                        column: x => x.AlocacaoId,
                        principalTable: "Alocaoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devolucao_Ferramentas_FerramentaPatrimonio",
                        column: x => x.FerramentaPatrimonio,
                        principalTable: "Ferramentas",
                        principalColumn: "Patrimonio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devolucao_AlocacaoId",
                table: "Devolucao",
                column: "AlocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucao_FerramentaPatrimonio",
                table: "Devolucao",
                column: "FerramentaPatrimonio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devolucao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ferramentas",
                table: "Ferramentas");

            migrationBuilder.AlterColumn<int>(
                name: "Patrimonio",
                table: "Ferramentas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Ferramentas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ferramentas",
                table: "Ferramentas",
                column: "Id");
        }
    }
}

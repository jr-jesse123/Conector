using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraEstrutura.Migrations
{
    public partial class corre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Devolucoes",
                table: "Alocaoes");

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

            migrationBuilder.AddColumn<string>(
                name: "Devolucoes",
                table: "Alocaoes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

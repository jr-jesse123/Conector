using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.InfraEstrutura.Migrations
{
    public partial class emprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Entrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Devolucao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FuncionarioCPF = table.Column<string>(type: "TEXT", nullable: false),
                    FerramentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Obra = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Ferramentas_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Ferramentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Funcionarios_FuncionarioCPF",
                        column: x => x.FuncionarioCPF,
                        principalTable: "Funcionarios",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_FerramentaId",
                table: "Emprestimos",
                column: "FerramentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_FuncionarioCPF",
                table: "Emprestimos",
                column: "FuncionarioCPF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimos");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.InfraEstrutura.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ferramentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeAbreviado = table.Column<string>(type: "TEXT", nullable: true),
                    Descrição = table.Column<string>(type: "TEXT", nullable: true),
                    DataCompra = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FotoUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferramentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Entrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FuncionarioCPF = table.Column<string>(type: "TEXT", nullable: false),
                    Obra = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Funcionarios_FuncionarioCPF",
                        column: x => x.FuncionarioCPF,
                        principalTable: "Funcionarios",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FerramentaEmprestada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataDevolucao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EmprestimoId = table.Column<int>(type: "INTEGER", nullable: true),
                    FerramentaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FerramentaEmprestada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FerramentaEmprestada_Emprestimos_EmprestimoId",
                        column: x => x.EmprestimoId,
                        principalTable: "Emprestimos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FerramentaEmprestada_Ferramentas_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Ferramentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_FuncionarioCPF",
                table: "Emprestimos",
                column: "FuncionarioCPF");

            migrationBuilder.CreateIndex(
                name: "IX_FerramentaEmprestada_EmprestimoId",
                table: "FerramentaEmprestada",
                column: "EmprestimoId");

            migrationBuilder.CreateIndex(
                name: "IX_FerramentaEmprestada_FerramentaId",
                table: "FerramentaEmprestada",
                column: "FerramentaId");

            migrationBuilder.CreateIndex(
                name: "IX_FerramentaEmprestada_Id",
                table: "FerramentaEmprestada",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FerramentaEmprestada");

            migrationBuilder.DropTable(
                name: "Emprestimos");

            migrationBuilder.DropTable(
                name: "Ferramentas");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}

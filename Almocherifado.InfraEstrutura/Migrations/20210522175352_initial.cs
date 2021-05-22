using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Almocherifado.InfraEstrutura.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ferramentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAbreviado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferramentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    cpfStr = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.cpfStr);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionariocpfStr = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Obra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermoResponsabilidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Funcionarios_FuncionariocpfStr",
                        column: x => x.FuncionariocpfStr,
                        principalTable: "Funcionarios",
                        principalColumn: "cpfStr",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FerramentaEmprestada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmprestimoId = table.Column<int>(type: "int", nullable: true),
                    DataDevolucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FerramentaId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_Emprestimos_FuncionariocpfStr",
                table: "Emprestimos",
                column: "FuncionariocpfStr");

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

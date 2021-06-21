using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraEstrutura.Migrations
{
    public partial class volta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Alocaoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsavelCPF = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContratoLocacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAlocacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alocaoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alocaoes_Funcionarios_ResponsavelCPF",
                        column: x => x.ResponsavelCPF,
                        principalTable: "Funcionarios",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ferramentas",
                columns: table => new
                {
                    Patrimonio = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fotos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmManutencao = table.Column<bool>(type: "bit", nullable: false),
                    Baixada = table.Column<bool>(type: "bit", nullable: false),
                    AlocacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferramentas", x => x.Patrimonio);
                    table.ForeignKey(
                        name: "FK_Ferramentas_Alocaoes_AlocacaoId",
                        column: x => x.AlocacaoId,
                        principalTable: "Alocaoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Devolucoes",
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
                    table.PrimaryKey("PK_Devolucoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devolucoes_Alocaoes_AlocacaoId",
                        column: x => x.AlocacaoId,
                        principalTable: "Alocaoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devolucoes_Ferramentas_FerramentaPatrimonio",
                        column: x => x.FerramentaPatrimonio,
                        principalTable: "Ferramentas",
                        principalColumn: "Patrimonio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alocaoes_ResponsavelCPF",
                table: "Alocaoes",
                column: "ResponsavelCPF");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucoes_AlocacaoId",
                table: "Devolucoes",
                column: "AlocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucoes_FerramentaPatrimonio",
                table: "Devolucoes",
                column: "FerramentaPatrimonio");

            migrationBuilder.CreateIndex(
                name: "IX_Ferramentas_AlocacaoId",
                table: "Ferramentas",
                column: "AlocacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devolucoes");

            migrationBuilder.DropTable(
                name: "Ferramentas");

            migrationBuilder.DropTable(
                name: "Alocaoes");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}

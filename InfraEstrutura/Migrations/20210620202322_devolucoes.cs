using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraEstrutura.Migrations
{
    public partial class devolucoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devolucao_Alocaoes_AlocacaoId",
                table: "Devolucao");

            migrationBuilder.DropForeignKey(
                name: "FK_Devolucao_Ferramentas_FerramentaPatrimonio",
                table: "Devolucao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devolucao",
                table: "Devolucao");

            migrationBuilder.RenameTable(
                name: "Devolucao",
                newName: "Devolucaos");

            migrationBuilder.RenameIndex(
                name: "IX_Devolucao_FerramentaPatrimonio",
                table: "Devolucaos",
                newName: "IX_Devolucaos_FerramentaPatrimonio");

            migrationBuilder.RenameIndex(
                name: "IX_Devolucao_AlocacaoId",
                table: "Devolucaos",
                newName: "IX_Devolucaos_AlocacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devolucaos",
                table: "Devolucaos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucaos_Alocaoes_AlocacaoId",
                table: "Devolucaos",
                column: "AlocacaoId",
                principalTable: "Alocaoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucaos_Ferramentas_FerramentaPatrimonio",
                table: "Devolucaos",
                column: "FerramentaPatrimonio",
                principalTable: "Ferramentas",
                principalColumn: "Patrimonio",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devolucaos_Alocaoes_AlocacaoId",
                table: "Devolucaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Devolucaos_Ferramentas_FerramentaPatrimonio",
                table: "Devolucaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devolucaos",
                table: "Devolucaos");

            migrationBuilder.RenameTable(
                name: "Devolucaos",
                newName: "Devolucao");

            migrationBuilder.RenameIndex(
                name: "IX_Devolucaos_FerramentaPatrimonio",
                table: "Devolucao",
                newName: "IX_Devolucao_FerramentaPatrimonio");

            migrationBuilder.RenameIndex(
                name: "IX_Devolucaos_AlocacaoId",
                table: "Devolucao",
                newName: "IX_Devolucao_AlocacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devolucao",
                table: "Devolucao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucao_Alocaoes_AlocacaoId",
                table: "Devolucao",
                column: "AlocacaoId",
                principalTable: "Alocaoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucao_Ferramentas_FerramentaPatrimonio",
                table: "Devolucao",
                column: "FerramentaPatrimonio",
                principalTable: "Ferramentas",
                principalColumn: "Patrimonio",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

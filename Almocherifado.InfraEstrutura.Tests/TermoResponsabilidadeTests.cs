using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura.Tests.TestesEmprestimoRepositoy;
using FluentAssertions;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class TermoResponsabilidadeTests
    {
        [Theory, DomainAutoData]
        public void Modelo_Eh_Encontrado_Corretamente(ModeloTermoService sut)
        {
            var termo = sut.GetModelo();
            termo.Text.Should().Contain(@"CONNECTOR ENGENHARIA LTDA, CNPJ nº 01.114.245/0001-02");
            
        }

        [Theory, DomainAutoData]
        public void Modelo_Eh_Editado_Corretamente(Emprestimo emprestimo, TermoResponsabilidadeService sut)
        {
            var termo = sut.GetTermo(emprestimo);
            termo.Text.Should().Contain(@"CONNECTOR ENGENHARIA LTDA, CNPJ nº 01.114.245/0001-02");

            termo.Text.Should().Contain(emprestimo.Funcionario.Nome);
            termo.Text.Should().Contain(emprestimo.Funcionario.CPF.ToString());
            termo.Text.Should().Contain(emprestimo.Obra);

            emprestimo.FerramentasEmprestas.ForEach(fe => termo.Text.Should().Contain(fe.Ferramenta.ToString()));

            termo.Text.Should().Contain(emprestimo.Entrega.Day.ToString("00"));
            termo.Text.Should().Contain(emprestimo.Entrega.Month.ToString("00"));
            termo.Text.Should().Contain(emprestimo.Entrega.Year.ToString("0000"));
        }




    }
}

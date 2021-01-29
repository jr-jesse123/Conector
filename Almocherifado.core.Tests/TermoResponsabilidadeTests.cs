using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Almocherifado.core.Tests
{
    public class TermoResponsabilidadeTests
    {
        [Theory, DomainAutoData]
        public void Modelo_Eh_Encontrado_Corretamente(ModeloTermoService sut)
        {
            var termo = sut.GetModelo();
            termo.Text.Should().Contain(@"CONNECTOR ENGENHARIA LTDA, CNPJ nº 01.114.245/0001-02");

            termo.SaveAs(@"D:\repos\Almocherifado\Conector\Almocherifado.core.Tests\bin\Debug\teste.docs");
        }

        [Theory, DomainAutoData]
        public void Modelo_Eh_Editado_Corretamente(DateTime entrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra , TermoResponsabilidadeService sut)
        {
            Debugger.Break();

            var termo = sut.GetTermoPreenchido(entrega, funcionario, ferramentas,Obra);
            termo.Text.Should().Contain(@"CONNECTOR ENGENHARIA LTDA, CNPJ nº 01.114.245/0001-02");

            termo.Text.Should().Contain(funcionario.Nome);
            termo.Text.Should().Contain(funcionario.CPF.ToString());
            termo.Text.Should().Contain(Obra);

            ferramentas.ForEach(fe => termo.Text.Should().Contain(fe.ToString()));

            termo.Text.Should().Contain(entrega.Day.ToString("00"));
            termo.Text.Should().Contain(entrega.Month.ToString("00"));
            termo.Text.Should().Contain(entrega.Year.ToString("0000"));
        }

        

    }
}

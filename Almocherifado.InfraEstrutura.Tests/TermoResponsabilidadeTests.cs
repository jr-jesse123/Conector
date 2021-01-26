using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura.Tests.TestesEmprestimoRepositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class TermoResponsabilidadeTests
    {
        [Theory, DomainAutoData]
        public void Teste_TermoResponsabilidade(Emprestimo emprestimo, TermoResponsabilidadeService sut)
        {

            var termo = sut.GetTermoResponsabilidade(emprestimo);




        }
    }
}

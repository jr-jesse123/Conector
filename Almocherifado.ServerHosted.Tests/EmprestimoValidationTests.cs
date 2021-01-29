using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.ServerHosted.Data.Models;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Almocherifado.ServerHosted.Tests.Mappingtests;

namespace Almocherifado.ServerHosted.Tests
{
    public class EmprestimoValidationTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        
        public EmprestimoValidationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }
        
        [Theory, UiAutoData]
        public void Validacao_Impede_Lista_Vazia_E_Nula(EmprestimoModel emprestimo)
        {

            emprestimo.Ferramentas = new List<Ferramenta>();

            var validator =   new EmprestimoValidator();
            var result = validator.TestValidate(emprestimo);

            result.Errors.Count.Should().Be(1);
            result.Errors.First().ErrorMessage.Should().Contain("ferramentas");

            emprestimo.Ferramentas = null;

            result = validator.TestValidate(emprestimo);

            result.Errors.Count.Should().Be(1);
            result.Errors.First().ErrorMessage.Should().Contain("ferramentas");


        }
    }
}

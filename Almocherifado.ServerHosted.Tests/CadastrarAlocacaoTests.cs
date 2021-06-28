using Almocherifado.UI.Components.Models;
using AutoFixture;
using AutoFixture.Xunit2;
using Bunit;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Almocherifado.UI.Tests
{

    public class InvalidAlocacaoGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Fixture()
                .Build<CadastroAlocacaoModel>()
                .Create()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CadastrarAlocacaoTests
    {
        private readonly ITestOutputHelper outputHelper;

        public CadastrarAlocacaoTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Theory, ClassData(typeof(InvalidAlocacaoGenerator))]
        public void Validacao_Impede_Cadastros_De_Alocacoes_Incorretas(CadastroAlocacaoModel model)
        {
            using var builder = new AlocarFerramentaComponentBuilder();
            using var cut = builder
                .WithRealValidator()
                .WithResponaveis(model.Responsavel)
                .Build();

            cut.Find("#responsavel").Change(model.Responsavel.CPF) ;

            cut.Find("#contratoLocacao").Change(model.ContratoLocacao);
            cut.Find("#dataLocacao").Change(model.Data);

            cut.Find("#SalvarBtn").Click();

            var messages = cut.Instance.form.EditContext.GetValidationMessages();

            messages.Should().HaveCount(1);
            messages.Should().Contain("Precisamos de ao menos uma ferramenta para realizar uma alocação");

        }


        


    }
}


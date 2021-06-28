using Almocherifado.UI.Components.Ferramentas;
using AutoFixture;
using Bunit;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Components;
using System;
using Almocherifado.UI.Components.Models;
using Almocherifado.UI.Components;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Almocherifado.UI.Tests
{
    public class InvalidModelGenerators : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            {
                new Fixture().Build<CadastroFerramentaModel>()
                .Without(cf => cf.Fotos)
                .Create()
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CadastrarFerramentasTests 
    {
        private readonly ITestOutputHelper outputHelper;

        public CadastrarFerramentasTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Theory]
        [ClassData(typeof(InvalidModelGenerators))]
        public void Validacao_Impede_Cadastros_De_Ferramentas_Incorretos(CadastroFerramentaModel model)
        {
            var cut = new CadastrarFerramentaBuilder()
                .WithRealValidator()
                .Build();

            model.Nome = "";

            new FerramentaValidator().Validate(model).IsValid.Should().BeFalse();

            //outputHelper.WriteLine(validator.IsValid.ToString());

            CadastrarFerramenta(model, cut);

            outputHelper.WriteLine(cut.Instance.form.EditContext.Model.ToString());

            cut.Find("#SalvarBtn").Click();

            IEnumerable<string> messages = cut.Instance.form.EditContext.GetValidationMessages();
            messages.Count().Should().BeGreaterThan(0);


            foreach (var item in messages)
            {
                outputHelper.WriteLine(item);
            }

            messages.Should().Contain("'Nome' deve ser informado.");
            messages.Should().Contain("'Patrimonio' deve ser informado.");
            messages.Should().Contain("É preciso adicionar ao menos uma foto");
            
        }

        [Fact]
        void CadastroDeFerramentasProduzFerramentaCorretamente()
        {   //arrange

            var ferramenta = new Fixture()
                .Build<CadastroFerramentaModel>()
                .Without(f => f.Fotos)
                .With(f => f.Patrimonio, 15)
                .With(f => f.DataDaCompra, new Fixture().Create<DateTime>().Date)
                .Create();

            using var builder = new CadastrarFerramentaBuilder();
            using var cut =  builder
                .ComProximoPatrimonio(15)
                .Build();

            CadastrarFerramenta(ferramenta, cut);

            var acutal = cut.Instance.FerramentaInput;
            
            ferramenta.Fotos = null;

            acutal.Should().Be(ferramenta);
        }

        private void CadastrarFerramenta(CadastroFerramentaModel ferramenta, IRenderedComponent<CadastrarFerramentas> cut)
        {

            //act
            cut.Find("#NomeInput")
                .Change(ferramenta.Nome);

            cut.Find("#MarcaInput")
                .Change(ferramenta.Marca);
            cut.Find("#ModeloInput")
                        .Change(ferramenta.Modelo);

            outputHelper.WriteLine(ferramenta.DataDaCompra.ToString());

            cut.Find("#DataDaCompraInput")
                    .Change(ferramenta.DataDaCompra.ToString("yyyy-MM-dd"));

            cut.Find("#DescricaoInput")
                        .Change(ferramenta.Descricao);
        }

      
    }
}

using AlmocharifadoApplication;
using Almocherifado.UI.Components.Ferramentas;
using AutoFixture;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Components;
using System;
using Almocherifado.UI.Components.Models;
using Almocherifado.UI.Components;
using AutoMapper;

namespace Almocherifado.UI.Tests
{
    public class CadastrarFerramentasTests
    {
        private readonly ITestOutputHelper outputHelper;

        public CadastrarFerramentasTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
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

            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            PrepararDependencias(ctx);
            var FerramentarepositoryStub = new Mock<IFerramentaRepository>();
            ctx.Services.AddSingleton(FerramentarepositoryStub.Object);
            var cut = ctx.RenderComponent<CadastrarFerramentas>();
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

        private static void PrepararDependencias(TestContext ctx, params Mock<object>[] servicos)
        {
            var proximoPatrimonioStub = new Mock<IProximoPatrimonioProvider>();
            var repositoryStub = new Mock<IAlmocharifadoRepository>();
            var mapperStub = new Mock<IMapper>();
            var validatorStub = new Mock<FluentValidation.AbstractValidator<CadastroFerramentaModel>>();
            var validator2 = new FerramentaValidator();

            proximoPatrimonioStub
                    .Setup(pp => pp.GetProximoPatrimonio()).Returns(15);

            ctx.Services.AddSingleton(proximoPatrimonioStub.Object);
            ctx.Services.AddSingleton(repositoryStub.Object);
            ctx.Services.AddSingleton(validatorStub.Object);
            ctx.Services.AddSingleton(mapperStub.Object);

            foreach (var item in servicos)
            {
                ctx.Services.AddSingleton(item.Object);
            }

        }
    }
}

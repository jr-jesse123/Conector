using AlmocharifadoApplication;
using Almocherifado.UI.Components.Ferramentas;
using AutoFixture;
using Bunit;
using Dtos;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Components;
using System;

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
        void test1() 
        {
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var ferramenta = new Fixture()
                .Build<FerramentaDto>()
                .Without(f => f.Fotos)
                .Without(f => f.Id)
                .With(f => f.Patrimonio, 15)
                .With(f => f.DataDaCompra, new Fixture().Create<DateTime>().Date)
                .Create();

            var proximoPatrimonioMOck = new Mock<IProximoPatrimonioProvider>();
            proximoPatrimonioMOck
            .Setup(pp => pp.GetProximoPatrimonio()).Returns(15);

            var repositoryMock = new Mock<IAlmocharifadoRepository>();
            //var sfLocalizerMock = new Mock<ISyncfusionStringLocalizer>();
            //var sfBlazorServiceMock = new Mock<SyncfusionBlazorService>();
            //var sfUploaderMock = new Mock<SfUploader>();
            

            ctx.Services.AddSingleton(proximoPatrimonioMOck.Object);
            ctx.Services.AddSingleton(repositoryMock.Object);
            //ctx.Services.AddSingleton(sfLocalizerMock.Object);
            //ctx.Services.AddSingleton(sfUploaderMock.Object);
            //ctx.Services.AddSingleton(sfBlazorServiceMock.Object);


            var cut = ctx.RenderComponent<CadastrarFerramentas>();



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


            outputHelper.WriteLine(cut.Instance.FerramentaInput.ToString());



            ferramenta.Fotos = null;
            
            cut.Instance.FerramentaInput.Should().Be(ferramenta);


            //outputHelper.WriteLine(cut.Markup);
        }        
    }
}

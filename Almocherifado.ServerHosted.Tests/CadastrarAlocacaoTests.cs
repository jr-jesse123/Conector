using AlmocharifadoApplication;
using Almocherifado.UI.Components.Alocacao;
using Almocherifado.UI.Components.Models;
using AutoFixture.Xunit2;
using AutoMapper;
using Bunit;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.UI.Tests
{
    public class CadastrarAlocacaoTests
    {

        [Theory, AutoData]
        public void Validacao_Impede_Cadastros_Incorretos()
        {
            
            var cut = new AlocarFerramentaComponentBuilder()                
                .Build();
            throw new NotImplementedException();
        }

        public class AlocarFerramentaComponentBuilder
        {
            public Mock<IFerramentaRepository> FerramentaRepositoryMock { get; set; } =
                new Mock<IFerramentaRepository>();
            public  Mock<IMapper> mapperStub { get; set; } = new Mock<IMapper>();
            public Mock<IValidator<CadastroAlocacaoModel>> validatorStub { get; set; } =
                    new Mock<IValidator<CadastroAlocacaoModel>>();
            public Mock<ISyncfusionStringLocalizer> localizerStrub { get; set; } =
                new Mock<ISyncfusionStringLocalizer>();
            public Mock<SyncfusionBlazorService> syncblazorStub { get; set; } = 
                new Mock<SyncfusionBlazorService>();
            public Mock<IAlmocharifadoRepository> almocharifadoStub { get; set; } =
                new Mock<IAlmocharifadoRepository>();
            public IRenderedComponent<AlocarFerramenta> Build()
            {

                using var ctx = new TestContext();

                ctx.Services.AddSingleton(FerramentaRepositoryMock.Object);
                ctx.Services.AddSingleton(almocharifadoStub.Object);
                ctx.Services.AddSingleton(validatorStub.Object);
                ctx.Services.AddSingleton(mapperStub.Object);
                ctx.Services.AddSingleton(localizerStrub.Object);
                ctx.Services.AddSingleton(syncblazorStub.Object);


                var cut = ctx.RenderComponent<AlocarFerramenta>(
                    );

                return cut;
            }
        }

        [Fact]
        public void CompoenenteRenderizaCorretamente()
        {
            using var ctx = new TestContext();
            var FerramentarepositoryStub = new Mock<IFerramentaRepository>();
            ctx.Services.AddSingleton(FerramentarepositoryStub.Object);
            var repositoryStub = new Mock<IAlmocharifadoRepository>();
            var mapperStub = new Mock<IMapper>();
            var validatorStub = new Mock<FluentValidation.IValidator<CadastroAlocacaoModel>>();
            validatorStub.Setup(v => v.Validate(It.IsAny<CadastroAlocacaoModel>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            var localizerStrub = new Mock<Syncfusion.Blazor.ISyncfusionStringLocalizer>();
            var syncblazorStub = new Mock<Syncfusion.Blazor.SyncfusionBlazorService>();

            ctx.Services.AddSingleton(repositoryStub.Object);
            ctx.Services.AddSingleton(validatorStub.Object);
            ctx.Services.AddSingleton(mapperStub.Object);
            ctx.Services.AddSingleton(localizerStrub.Object);
            ctx.Services.AddSingleton(syncblazorStub.Object);


            var cut = ctx.RenderComponent<AlocarFerramenta>(

                );
        }




    }
}

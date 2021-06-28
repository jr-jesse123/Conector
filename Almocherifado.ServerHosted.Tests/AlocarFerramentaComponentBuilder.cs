using ]7
    7
    AlmocharifadoApplication;
using Almocherifado.UI.Components.Alocacao;
using Almocherifado.UI.Components.Models;
using AutoMapper;
using Bunit;
using Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OneOf;
using Syncfusion.Blazor;
using System;

namespace Almocherifado.UI.Tests
{
    public class AlocarFerramentaComponentBuilder :IDisposable
    {
        TestContext ctx = new TestContext();
        public 
            Mock<IFerramentaRepository> FerramentaRepositoryMock { get; set; } =
            new Mock<IFerramentaRepository>();
        public Mock<IMapper> mapperStub { get; set; } = new Mock<IMapper>();
        
        public 
            OneOf<Mock<IValidator<CadastroAlocacaoModel>>, 
                CadastroAlocacaoModelValidator> validatorStub { get; set; } =
                    new Mock<IValidator<CadastroAlocacaoModel>>();
        public Mock<ISyncfusionStringLocalizer> localizerStrub { get; set; } =
            new Mock<ISyncfusionStringLocalizer>();
        public Mock<SyncfusionBlazorService> syncblazorStub { get; set; } =
            new Mock<SyncfusionBlazorService>();
        public Mock<IAlmocharifadoRepository> almocharifadoRepositoryStub { get; set; } =
            new Mock<IAlmocharifadoRepository>();
        public IRenderedComponent<AlocarFerramenta> Build()
        {

            

            validatorStub.Match(
                stub => ctx.Services.AddSingleton(stub.Object),
                validator => ctx.Services.AddSingleton(validator)
                );

            ctx.Services.AddSingleton(FerramentaRepositoryMock.Object);
            ctx.Services.AddSingleton(almocharifadoRepositoryStub.Object);
            
            ctx.Services.AddSingleton(mapperStub.Object);
            ctx.Services.AddSingleton(localizerStrub.Object);
            ctx.Services.AddSingleton(syncblazorStub.Object);


            var cut = ctx.RenderComponent<AlocarFerramenta>(
                );

            return cut;
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        internal AlocarFerramentaComponentBuilder WithRealValidator()
        {
            validatorStub = new CadastroAlocacaoModelValidator();
            return this;
        }

        internal AlocarFerramentaComponentBuilder WithResponaveis(params Funcionario[] responsaveis)
        {
            almocharifadoRepositoryStub.Setup(ar => ar.GetAllFuncionarios())
                .Returns(responsaveis);

            //throw new NotImplementedException();
            return this;
        }
    }
}

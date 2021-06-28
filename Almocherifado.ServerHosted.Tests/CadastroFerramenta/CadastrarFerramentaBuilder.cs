using AlmocharifadoApplication;
using Almocherifado.UI.Components.Ferramentas;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Almocherifado.UI.Components.Models;
using AutoMapper;
using FluentValidation;
using OneOf;
using System;

namespace Almocherifado.UI.Tests
{
    public class CadastrarFerramentaBuilder : IDisposable
    {
        public TestContext ctx { get; set; } = new TestContext();
        public Mock<IProximoPatrimonioProvider> proximoPatrimonioStub { get; set; } =
            new Mock<IProximoPatrimonioProvider>();
        public Mock<IAlmocharifadoRepository> repositoryStub { get; set; } =
            new Mock<IAlmocharifadoRepository>();
        public Mock<IMapper> mapperStub { get; set; } = 
            new Mock<IMapper>();

        public OneOf<Mock<AbstractValidator<CadastroFerramentaModel>>,
                        CadastroAlocacaoModelValidator> validatorStub { get; set; } =
                new Mock<AbstractValidator<CadastroFerramentaModel>>();

        //public FerramentaValidator validator2 = new FerramentaValidator();

        public CadastrarFerramentaBuilder WithRealValidator()
        {
            validatorStub = new CadastroAlocacaoModelValidator();
            return this;
        }

        public CadastrarFerramentaBuilder()
        {
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;   
        }

        public CadastrarFerramentaBuilder ComProximoPatrimonio(int ProximoPatrimonio)
        {
            proximoPatrimonioStub.Setup(pp => pp.GetProximoPatrimonio()).Returns(ProximoPatrimonio);
            return this;
        }
        
        public IRenderedComponent<CadastrarFerramentas> Build()
        {
            ctx.Services.AddSingleton(proximoPatrimonioStub.Object);
            ctx.Services.AddSingleton(repositoryStub.Object);
            ctx.Services.AddSingleton(mapperStub.Object);


            validatorStub.Match(
                mock => ctx.Services.AddSingleton(mock.Object),
                validator => ctx.Services.AddSingleton(validator)
                );

            


            //PrepararDependencias(ctx);
            var FerramentarepositoryStub = new Mock<IFerramentaRepository>();
            ctx.Services.AddSingleton(FerramentarepositoryStub.Object);

            return ctx.RenderComponent<CadastrarFerramentas>();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

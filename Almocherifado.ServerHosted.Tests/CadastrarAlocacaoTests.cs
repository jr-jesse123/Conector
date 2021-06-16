using AlmocharifadoApplication;
using Almocherifado.UI.Components.Alocacao;
using Almocherifado.UI.Components.Models;
using AutoMapper;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.UI.Tests
{
    public class CadastrarAlocacaoTests
    {
        [Fact]
        public void CompoenenteRenderizaCorretamente()
        {
            using var ctx = new TestContext();

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

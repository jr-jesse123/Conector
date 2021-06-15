using AlmocharifadoApplication;
using Almocherifado.UI.Components;
using Almocherifado.UI.Components.Funcionarios;
using Almocherifado.UI.Components.Models;
using AutoFixture;
using AutoMapper;
using Bunit;
using Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.UI.Tests
{
    public class CadastrarFuncionarioTests
    {

        //Arrange
        [Fact]
        public void CadastroDeFuncionarioFuncionaCorretamente()
        {
            var fotoStub = new Mock<UploadFiles>();

            
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var repositoryStub = new Mock<IAlmocharifadoRepository>();
            var mapperStub = new Mock<IMapper>();
            var validatorStub = new Mock<FluentValidation.IValidator<CadastroFuncionarioModel>>();
            validatorStub.Setup(v => v.Validate(It.IsAny<CadastroFuncionarioModel>()))
                .Returns(new FluentValidation.Results.ValidationResult());
            
            var uploaderStub = new Mock<IRenderedFragment>();
            var localizerStrub = new Mock<Syncfusion.Blazor.ISyncfusionStringLocalizer>();
            var syncblazorStub = new Mock<Syncfusion.Blazor.SyncfusionBlazorService>();

            ctx.Services.AddSingleton(repositoryStub.Object);
            ctx.Services.AddSingleton(validatorStub.Object);
            ctx.Services.AddSingleton(mapperStub.Object);
            ctx.Services.AddSingleton(localizerStrub.Object);
            ctx.Services.AddSingleton(syncblazorStub.Object);

            var cut = ctx.RenderComponent<CadastrarFuncionario>(
                ComponentParameterFactory
                    .RenderFragment<Uploader>("Uploader")
                ); ;

            var mockUploadFile = new Mock<UploaderFiles>();

            var funcionarioModelo = new Fixture()
                    .Build<CadastroFuncionarioModel>()
                    .Without(f => f.Foto)
                    .With(f => f.Cargo, "Nenhum")
                    .Create();

            cut.Find("#NomeInput")
                .Change(funcionarioModelo.Nome);

            cut.Find("#CargoInput")
                .Change(funcionarioModelo.Cargo);

            cut.Find("#EmailInput")
                .Change(funcionarioModelo.Email);

            cut.Find("#NomeInput")
                .Change(funcionarioModelo.Nome);




            //cut.Instance.Form.Model.Should().Be(funcionarioModelo);


            //repositoryStub.Verify(repo => repo.SalvarFuncionario(funcionarioModelo));

        }
    }
}

using Almocharifado.InfraEstrutura;
using AlmocharifadoApplication;
using Almocherifado.UI.Components;
using Almocherifado.UI.Components.Funcionarios;
using Almocherifado.UI.Components.Models;
using AutoFixture;
using AutoMapper;
using Bunit;
using Entities;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OneOf;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Almocherifado.UI.Tests
{
    public class InvalidFuncionariosGernerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Fixture()
                .Build<CadastroFuncionarioModel>()
                .Without(cf => cf.Foto)
                .Create()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CadastrarFuncionarioComponentBuilder  : IDisposable
    {
        public TestContext ctx { get; set; } = new TestContext();
        public Mock<UploadFiles> fotoStub { get; set; } = new Mock<UploadFiles>();
        public Mock<IFerramentaRepository> FerramentarepositoryStub { get; set; } = new Mock<IFerramentaRepository>();
        public Mock<IAlmocharifadoRepository> repositoryStub { get; set; } = new Mock<IAlmocharifadoRepository>();
        public Mock<IMapper> mapperStub { get; set; } = new Mock<IMapper>();
        public  OneOf<Mock<IValidator<CadastroFuncionarioModel>>, FuncionarioValidator> validatorStub { get; set; } =
            new Mock<IValidator<CadastroFuncionarioModel>>();
        public Mock<ISyncfusionStringLocalizer> localizerStrub { get; set; } = new Mock<ISyncfusionStringLocalizer>();
        public Mock<SyncfusionBlazorService> syncblazorStub { get; set; } = new Mock<SyncfusionBlazorService>();
        public Mock<IRenderedFragment> uploaderStub { get; set; } = new Mock<IRenderedFragment>();

        public CadastrarFuncionarioComponentBuilder()
        {
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        public CadastrarFuncionarioComponentBuilder WithRealValidator()
        {
            validatorStub = new FuncionarioValidator();
            return this;
        }

        public IRenderedComponent<CadastrarFuncionario> Build()
        {

            //validatorStub.Setup(v => v.Validate(It.IsAny<CadastroFuncionarioModel>()))
            //    .Returns(new FluentValidation.Results.ValidationResult());

            validatorStub.Match(
                mock => ctx.Services.AddSingleton(mock.Object),
                validator => ctx.Services.AddSingleton(validator)
                );

            //ctx.Services.AddSingleton(validatorStub.Object);

            ctx.Services.AddSingleton(FerramentarepositoryStub.Object);
            ctx.Services.AddSingleton(repositoryStub.Object);
            
            ctx.Services.AddSingleton(mapperStub.Object);
            ctx.Services.AddSingleton(localizerStrub.Object);
            ctx.Services.AddSingleton(syncblazorStub.Object);

            var cut = ctx.RenderComponent<CadastrarFuncionario>(
                ComponentParameterFactory
                    .RenderFragment<Uploader>("Uploader")
                ); ;

            return cut;

        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }

    public class CadastrarFuncionarioTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public CadastrarFuncionarioTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Theory,ClassData(typeof(InvalidFuncionariosGernerator))]
        public void Validacao_impede_Cadastros_de_Funcionarios_Invalidos(CadastroFuncionarioModel  model)
        {
            using var builder = new CadastrarFuncionarioComponentBuilder();

            using var cut = builder
                .WithRealValidator()
                .Build();

            CadastrarFuncionarioo(model, cut);

            cut.Find("#SalvarBtn").Click();

            var messages = cut.Instance
                .form
                .EditContext
                .GetValidationMessages()
                ;
            messages.Count().Should().BeGreaterThan(0);

            messages.Should().Contain("'Email' é um endereço de email inválido.")
                .And.Contain("O CPF é inválido!")
                .And.Contain("'Foto' deve ser informado.");

        }
        
        [Fact]
        public void CadastroDeFuncionarioFuncionaCorretamente()
        {

            //var mockUploadFile = new Mock<UploaderFiles>();

            var funcionarioModelo = new Fixture()
                    .Build<CadastroFuncionarioModel>()
                    .Without(f => f.Foto)
                    .With(f => f.Cargo, "Nenhum")
                    .Create();

            using var builder = new CadastrarFuncionarioComponentBuilder();
            using var cut = builder.Build();
            CadastrarFuncionarioo(funcionarioModelo, cut);




            //cut.Instance.Form.Model.Should().Be(funcionarioModelo);


            //repositoryStub.Verify(repo => repo.SalvarFuncionario(funcionarioModelo));

        }

        private static void CadastrarFuncionarioo(CadastroFuncionarioModel funcionarioModelo, IRenderedComponent<CadastrarFuncionario> cut)
        {
            cut.Find("#NomeInput")
                .Change(funcionarioModelo.Nome);

            cut.Find("#CargoInput")
                .Change(funcionarioModelo.Cargo);

            cut.Find("#EmailInput")
                .Change(funcionarioModelo.Email);

            cut.Find("#NomeInput")
                .Change(funcionarioModelo.Nome);
        }
    }
}

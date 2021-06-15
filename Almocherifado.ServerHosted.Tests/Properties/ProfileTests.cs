using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Almocherifado.UI.Components.Profiles;
using AutoFixture;
using Almocherifado.UI.Components.Models;
using Entities;
using Moq;
using Syncfusion.Blazor.Inputs;
using Xunit.Abstractions;
using FluentAssertions;
using Almocherifado.UI.Components.Helpers;

namespace Almocherifado.UI.Tests.Properties
{
    public class ProfileTests
    {
        private readonly ITestOutputHelper outputHelper;

        public ProfileTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }


        [Fact]
        public void FerramentaModelEhConvertidaParaDominioCorretamente()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LeanProfile());
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            var fotosStub = new Mock<UploadFiles>();

            var ferramentaModel = new Fixture()
                    .Build<CadastroFerramentaModel>()
                    .With(cf => cf.Fotos, new UploadFiles[] { fotosStub.Object })
                    .Create();

            var domain = mapper.Map<Ferramenta>(ferramentaModel);

            domain.DataCompra.Should().Be(ferramentaModel.DataDaCompra);
            domain.Descricao.Should().Be(ferramentaModel.Descricao);

            var indice = 1;
            domain.Fotos.ToList().ForEach(ft => ft.Should().Be(FileHelper.getPath(ferramentaModel, indice++)));

            
            domain.Marca.Should().Be(ferramentaModel.Marca);
            domain.Modelo.Should().Be(ferramentaModel.Modelo);
            domain.Nome.Should().Be(ferramentaModel.Nome);
            domain.Patrimonio.Should().Be(ferramentaModel.Patrimonio);
        }



        [Fact]
        public void FuncionarioMOdelEhConvertidaParaDominioCorretamente()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LeanProfile());
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            var fotosStub = new Mock<UploadFiles>();

            fotosStub.Setup(f => f.ToString()).Returns("stub");

            var funcionarioModel = new Fixture()
                    .Build<CadastroFuncionarioModel>()
                    .With(cf => cf.Foto,  fotosStub.Object )
                    .Create();

            var domain = mapper.Map<Funcionario>(funcionarioModel);

            domain.Cargo.Should().Be(funcionarioModel.Cargo);
            domain.CPF.Should().Be(funcionarioModel.CPF);

            var indice = 1;
            domain.Foto.Should().Be(FileHelper.getFotFuncionarioPath(funcionarioModel));


            domain.Email.Should().Be(funcionarioModel.Email);
            domain.Nome.Should().Be(funcionarioModel.Nome);
            

        }
    }
}

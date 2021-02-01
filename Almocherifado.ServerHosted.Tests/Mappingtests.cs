using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.Tests;
using Almocherifado.UI.Data.Models;
using Almocherifado.UI.Data.Models.MappingProfiles;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Almocherifado.core.AgregateRoots.FuncionarioNm;

namespace Almocherifado.UI.Tests
{
    public partial class Mappingtests
    {
        MapperConfiguration config;
        private IMapper mapper;

        public Mappingtests()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
            mapper = config.CreateMapper();
        }

        [Fact]
        public void Configuration_is_Valid()
        {
            config.AssertConfigurationIsValid();
        }
        
        [Theory, DomainAutoData]
        public void Conversao_De_FerramentaModel_Para_Ferramenta_eh_Valida(FerramentaModel model)
        {   
            var FerramentaEntity = mapper.Map<FerramentaModel, Ferramenta>(model);

            FerramentaEntity.NomeAbreviado.Should().BeEquivalentTo(model.NomeAbreviado);
            FerramentaEntity.DataCompra.Should().BeSameDateAs(model.DataCompra);
            FerramentaEntity.Descricao.Should().BeEquivalentTo(model.Descricao);
            FerramentaEntity.FotoUrl.Should().BeEquivalentTo(model.FotoUrl);   
        }



        [Theory, DomainAutoData]
        public void Conversao_De_Ferramenta_Para_FerramentaModel_eh_Valida(Ferramenta ferramenta)
        {   
            var FerramentModel = mapper.Map<Ferramenta, FerramentaModel>(ferramenta);

            FerramentModel.NomeAbreviado.Should().BeEquivalentTo(ferramenta.NomeAbreviado);
            FerramentModel.DataCompra.Should().BeSameDateAs(ferramenta.DataCompra);
            FerramentModel.Modelo.Should().BeEquivalentTo(ferramenta.Modelo);
            FerramentModel.Marca.Should().BeEquivalentTo(ferramenta.Marca);
            FerramentModel.FotoUrl.Should().BeEquivalentTo(ferramenta.FotoUrl);
            FerramentModel.Descricao.Should().BeEquivalentTo(ferramenta.Descricao);
        }

        [Theory, UiAutoData]
        public void Conversao_De_Emprestimo_Para_EmprestimoModelModel_eh_Valida(EmprestimoModel emprestmioModel, List<Ferramenta>  ferramentas)
        {
            var emprestimoObject = mapper.Map<EmprestimoModel, Emprestimo>(emprestmioModel);
            
            emprestimoObject.Entrega.Should().NotBe(default(DateTime));
            emprestimoObject.Finalizado.Should().BeFalse();
            emprestimoObject.Funcionario.Should().Be(mapper.Map<Funcionario>(emprestmioModel.Funcionario));
            emprestimoObject.Funcionario.CPF.ToString().Should().Be(emprestmioModel.Funcionario.CPF);
            emprestimoObject.Obra.Should().Be(emprestmioModel.Obra);
            emprestimoObject.Entrega.Should().Be(emprestmioModel.entrega);

            emprestimoObject.FerramentasEmprestas.ForEach(e => e.AcusarRecebimento());

            emprestimoObject.Finalizado.Should().BeTrue();

        }
    }
    
}

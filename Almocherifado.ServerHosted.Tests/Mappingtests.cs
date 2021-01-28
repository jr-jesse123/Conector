using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.Tests;
using Almocherifado.ServerHosted.Data.Models;
using Almocherifado.ServerHosted.Data.Models.MappingProfiles;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Almocherifado.ServerHosted.Tests
{
    public class Mappingtests
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

        [Theory, DomainAutoData]
        public void Conversao_De_Emprestimo_Para_EmprestimoModelModel_eh_Valida(EmprestimoModel emprestmioModel, List<Ferramenta>  ferramentas)
        {
            var emprestimoObject = mapper.Map<EmprestimoModel, Emprestimo>(emprestmioModel);

            emprestimoObject.Entrega.Should().NotBe(default(DateTime));

            emprestimoObject.Finalizado.Should().BeFalse();

        }

        class UiAutoData : DomainAutoDataAttribute
        {
            
        }

    }
}

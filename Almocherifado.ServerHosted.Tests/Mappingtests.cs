using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.ServerHosted.Data.Models;
using Almocherifado.ServerHosted.Data.Models.MappingProfiles;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using System;
using Xunit;

namespace Almocherifado.ServerHosted.Tests
{
    public class Mappingtests
    {
        MapperConfiguration config;

        public Mappingtests()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
        }

        [Fact]
        public void Configuration_is_Valid()
        {
            config.AssertConfigurationIsValid();
        }
        
        [Theory, AutoData]
        public void Conversao_De_FerramentaModel_Para_Ferramenta_eh_Valida(FerramentaModel model)
        {   
            var mapper = config.CreateMapper();
            var FerramentaEntity = mapper.Map<FerramentaModel, Ferramenta>(model);

            FerramentaEntity.NomeAbreviado.Should().BeEquivalentTo(model.NomeAbreviado);
            FerramentaEntity.DataCompra.Should().BeSameDateAs(model.DataCompra);
            FerramentaEntity.Descricao.Should().BeEquivalentTo(model.Descricao);

            FerramentaEntity.FotoUrl.Should().BeEquivalentTo(model.FotoUrl);
            
        }



        [Theory, AutoData]
        public void Conversao_De_Ferramenta_Para_FerramentaModel_eh_Valida(Ferramenta ferramenta)
        {

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
            var mapper = config.CreateMapper();
            var FerramentModel = mapper.Map<Ferramenta, FerramentaModel>(ferramenta);

            FerramentModel.NomeAbreviado.Should().BeEquivalentTo(ferramenta.NomeAbreviado);
            FerramentModel.DataCompra.Should().BeSameDateAs(ferramenta.DataCompra);
            
            FerramentModel.Modelo.Should().BeEquivalentTo(ferramenta.Modelo);
            
            FerramentModel.Marca.Should().BeEquivalentTo(ferramenta.Marca);


            FerramentModel.FotoUrl.Should().BeEquivalentTo(ferramenta.FotoUrl);
            FerramentModel.Descricao.Should().BeEquivalentTo(ferramenta.Descricao);


            

        }

    }
}

using Almocherifado.core;
using Almocherifado.ServerHosted.Data.Models;
using Almocherifado.ServerHosted.Data.Models.MappingProfiles;
using AutoMapper;
using FluentAssertions;
using System;
using Xunit;

namespace Almocherifado.ServerHosted.Tests
{
    public class Mappingtests
    {
       [Fact]
        public void Configuration_is_Valid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
            config.AssertConfigurationIsValid();
        }
        
        [Fact]
        public void Conversao_De_Ferramenta_Para_Model_eh_Valida()
        {
            var ferramentaModel = new FerramentaModel { DataCompra = DateTime.Now, Descrição = "Ferramenta boa", FotoUrl = "\\fotos\\foto1", NomeAbreviado = "Ferramenta1" };
            
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
            var mapper = config.CreateMapper();
            var FerramentaEntity = mapper.Map<FerramentaModel, Ferramenta>(ferramentaModel);

            FerramentaEntity.NomeAbreviado.Should().BeEquivalentTo(ferramentaModel.NomeAbreviado);
            FerramentaEntity.DataCompra.Should().BeSameDateAs(ferramentaModel.DataCompra);
            FerramentaEntity.Descrição.Should().BeEquivalentTo(ferramentaModel.Descrição);

            FerramentaEntity.FotoUrl.Should().BeEquivalentTo(ferramentaModel.FotoUrl);
            
        }


    }
}

using Almocherifado.core.AgregateRoots.FerramentaNm;
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
        public void Conversao_De_FerramentaModel_Para_Ferramenta_eh_Valida()
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



        [Fact]
        public void Conversao_De_Ferramenta_Para_FerramentaModel_eh_Valida()
        {
            var ferramenta = new Ferramenta("Ferramenta1", "Ferramenta boa", DateTime.Now, "\\fotos\\foto1","brastemp","chave");

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
            var mapper = config.CreateMapper();
            var FerramentModel = mapper.Map< Ferramenta, FerramentaModel>(ferramenta);

            FerramentModel.NomeAbreviado.Should().BeEquivalentTo(ferramenta.NomeAbreviado);
            FerramentModel.DataCompra.Should().BeSameDateAs(ferramenta.DataCompra);
            FerramentModel.Descrição.Should().BeEquivalentTo(ferramenta.Descrição);

            FerramentModel.FotoUrl.Should().BeEquivalentTo(ferramenta.FotoUrl);

        }

    }
}

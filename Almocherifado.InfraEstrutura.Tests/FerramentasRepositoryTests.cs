using Almocherifado.core.AgregateRoots.FerramentaNm;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Xunit.Abstractions;
using Almocherifado.InfraEstrutura.Tests.Fixtures;
using Almocherifado.InfraEstrutura.Repositorios;
using System.Linq;
using FluentAssertions;
using Moq;
using AutoFixture.Xunit2;
using AutoFixture;
using Almocherifado.core.AgregateRoots.FuncionarioNm;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class FerramentasRepositoryTests : IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;
        private AlmocherifadoContext testContext;

        public FerramentasRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>()
           .UseSqlite(@"Data Source=TestesFerramentas.db")
           .UseLazyLoadingProxies().Options;
           
            testContext = new AlmocherifadoContext(options);
            testContext.Database.EnsureDeleted();

            testContext.Database.Migrate();
            
            // this.testOutputHelper = testOutputHelper;
        }

        [Theory]
        [AutoMoqData()]
        public void Com_Banco_Vazio_Conseguirmos_Adicionar_Ferramenta(Ferramenta ferramenta)
        {   
            FerramentaRepository sut = new (testContext);

            var count = sut.GetallFerramentas().Count();

            sut.AdicionarFerramenta(ferramenta);

            sut.GetallFerramentas().Count().Should().Be(++count);
            //var fixture = new Fixture();

            var ferramentapersistida = sut.GetallFerramentas().Last();
            ferramentapersistida.Should().BeEquivalentTo(ferramenta, "este foi o funcionário adicionado");

        }

        public void Dispose()
        {
            testContext.Database.EnsureDeleted();
            testContext.Dispose();
        }

        [Theory]
        [AutoData]
        public void Ferramenta_Deve_Ser_Deletado(Ferramenta ferramenta1, Ferramenta ferramenta2)
        {
            IFerramentaRepository sut = new FerramentaRepository(testContext);
            var count = sut.GetallFerramentas().Count();

            sut.AdicionarFerramenta(ferramenta2);
            sut.AdicionarFerramenta(ferramenta1);
            
            sut.DeletarFerramenta(ferramenta2);

            sut.GetallFerramentas().Count().Should().Be(++count, "haviam dois funcionários e um foi dleetado");

        }

    }
}

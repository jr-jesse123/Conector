using Almocherifado.core;
using Almocherifado.InfraEstrutura.Repositorios;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class EmprestimosRepositoryTests
    {
        [Fact]
        public void tese1()
        {

            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>()
                .UseInMemoryDatabase("test2").Options;

            using AlmocherifadoContext memoryContext = new AlmocherifadoContext(options);
            memoryContext.Database.EnsureDeleted();
            memoryContext.Database.EnsureCreated();


            IFerramentaRepository repository = new FerramentaRepository(memoryContext);

            var ferramenta = new Ferramenta("Ferramenta1", "Pense numa ferramenta da boa", DateTime.Today.AddDays(-7), @"/fotos/foto");

            repository.AdicionarFerramenta(ferramenta);

            repository.GetallFerramentas().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var ferramentapersistida = repository.GetallFerramentas().First();
            ferramentapersistida.Should().BeEquivalentTo(ferramenta, "este foi o funcionário adicionado");

        }
    }

    public class TestSetup
    {

    }
}

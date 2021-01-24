using Almocherifado.Application;
using Almocherifado.core;
using Almocherifado.InfraEstrutura;
using Almocherifado.InfraEstrutura.Repositorios;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Almocherifado.Application.Tests
{
    public class FerramentasServicesTests : IDisposable
    {
        private AlmocherifadoContext context;
        private FerramentaRepository repository;
        private FerramentasService service;

        public FerramentasServicesTests()
        {
            var options = new DbContextOptionsBuilder<AlmocherifadoContext>().UseSqlite(@"Data Source = TestesFerramentaServcie.db;").Options;
            context = new AlmocherifadoContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new FerramentaRepository(context);

            service = new FerramentasService(repository);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void Ferramenta_Eh_Identificada_Corretamente()
        {
            var ferramenta1 = new Ferramenta("Ferramenta1", "eh da boa", DateTime.Now, "foto/foto");

            repository.AdicionarFerramenta(ferramenta1);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeTrue(" Esta ferramenta foi adicionada anteriomente");

        }


        
    }
}

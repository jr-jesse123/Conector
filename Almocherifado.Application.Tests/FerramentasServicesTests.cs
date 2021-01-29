using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.InfraEstrutura;
using Almocherifado.InfraEstrutura.Repositorios;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using Almocherifado.core.Tests;

namespace Almocherifado.Application.Tests
{
    public class FerramentasServicesTests : IDisposable
    {
        private AlmocherifadoContext context;
        private FerramentaRepository repository;
        private FerramentasService service;
        private Funcionario Funcionario;

        public FerramentasServicesTests()
        {
            var options = new DbContextOptionsBuilder<AlmocherifadoContext>().UseSqlite(@"Data Source = TestesFerramentaServcie.db;").Options;
            context = new AlmocherifadoContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new FerramentaRepository(context);

            service = new FerramentasService(context);
 
        }


        [Theory, DomainAutoData]
        public void Ferramenta_Emprestada_Eh_Reconhecida_Corretamente(Ferramenta ferramenta1, Emprestimo emprestimo)
        {
            repository.AdicionarFerramenta(ferramenta1);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeFalse("Esta Ferramenta ainda não está emprestada");

            var emprestimoRepository = new EmprestimosRepository(context);

            emprestimoRepository.SalvarNovoEmprestimo(emprestimo);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeTrue("Esta Ferramenta Foi Emprestada");
        }

        [Theory, DomainAutoData]
        public void Ferramenta_Devolvida_Eh_Reconhecida_Corretamente(Ferramenta ferramenta1, Emprestimo emprestimo)
        {
            repository.AdicionarFerramenta(ferramenta1);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeFalse("Esta Ferramenta ainda não está emprestada");

            var emprestimoRepository = new EmprestimosRepository(context);

            emprestimoRepository.SalvarNovoEmprestimo(emprestimo);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeTrue("Esta Ferramenta Foi Emprestada");

            emprestimo.Finalizado.Should().BeFalse("Este empréstimo ainda não está finalizado");

            emprestimo.FerramentasEmprestas.First().AcusarRecebimento();

            emprestimoRepository.EditarEmprestimo(emprestimo);

            emprestimo.Finalizado.Should().BeTrue("Este Emprestimo Foi finalizado");

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeFalse("Esta Ferramenta Já está disponível novamente");
        }


        [Theory, DomainAutoData]
        public void Teste_Com_Varias_Ferramentas_No_Mesmo_Emprestimo(Ferramenta ferramenta1, Ferramenta ferramenta2, Emprestimo emprestimo)
        {
            repository.AdicionarFerramenta(ferramenta1);
            repository.AdicionarFerramenta(ferramenta2);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeFalse("Esta Ferramenta ainda não está emprestada");
            service.VerificarSeFerramentaEstaEmprestada(ferramenta2).Should().BeFalse("Esta Ferramenta ainda não está emprestada");

            var emprestimoRepository = new EmprestimosRepository(context);

            emprestimoRepository.SalvarNovoEmprestimo(emprestimo);

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeTrue("Esta Ferramenta Foi Emprestada");


            emprestimo.Finalizado.Should().BeFalse("Este empréstimo ainda não está finalizado");

            emprestimo.FerramentasEmprestas.First().AcusarRecebimento();
            emprestimoRepository.EditarEmprestimo(emprestimo);


            emprestimo.Finalizado.Should().BeFalse("Este empréstimo ainda não está finalizado");

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeFalse("Esta Ferramenta Foi Emprestada");

            emprestimo.FerramentasEmprestas[1].AcusarRecebimento();

            emprestimoRepository.EditarEmprestimo(emprestimo);

            emprestimo.Finalizado.Should().BeTrue("Este Emprestimo Foi finalizado");

            service.VerificarSeFerramentaEstaEmprestada(ferramenta1).Should().BeFalse("Esta Ferramenta Já está disponível novamente");
        }


        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

    }
}

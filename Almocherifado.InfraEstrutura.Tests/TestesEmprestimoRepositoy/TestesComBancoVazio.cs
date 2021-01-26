using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Tests;
using Almocherifado.InfraEstrutura.Repositorios;
using AutoFixture;
using AutoFixture.Kernel;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests.TestesEmprestimoRepositoy
{
    public partial class TestesComBancoVazio :  IDisposable
    {
        
        
        private AlmocherifadoContext TestContext;
        //private Fixture Fixture;

        public TestesComBancoVazio()
        {
            var con = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TesteEmprestimo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>()
                
                .UseSqlite(@$"Data Source = Testesalmocherifado.db;").Options;


            TestContext = new AlmocherifadoContext(options);
            TestContext.Database.EnsureDeleted();

            TestContext.Database.Migrate();

            var Fixture = new Fixture();


        }


        [Theory, DomainAutoData]
        public void Emprestimo_Eh_Adicionado_Com_Sucesso(Emprestimo emprestimo)
        {
            EmprestimosRepository sut = new EmprestimosRepository(TestContext);
            //var ferramenta = new Ferramenta("Ferramenta1", "Pense numa ferramenta da boa", DateTime.Today.AddDays(-7), @"/fotos/foto");

            sut.SalvarNovoEmprestimo(emprestimo);

            sut.GetAllEmprestimos().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var emprestimoPersistido = sut.GetAllEmprestimos().First();


            emprestimo.Should().BeEquivalentTo(emprestimo, "este foi o funcionário adicionado");
        }

        [Theory, DomainAutoData]
        public void Emprestimo_Eh_Editado_Finalizado_E_Persistido_Com_Sucesso(Fixture fixture)
        {
            var emprestimo = fixture.Create<Emprestimo>() ;

            EmprestimosRepository sut = new (TestContext);
            sut.SalvarNovoEmprestimo(emprestimo);
            //var ferramenta = new Ferramenta("Ferramenta1", "Pense numa ferramenta da boa", DateTime.Today.AddDays(-7), @"/fotos/foto");

            emprestimo.Finalizado.Should().BeFalse(" O emprestimo ainda tem ferramentas alocadas");

            emprestimo.FerramentasEmprestas.First().AcusarRecebimento();

            emprestimo.Finalizado.Should().BeFalse(" O emprestimo ainda tem ferramentas alocadas");

            emprestimo.FerramentasEmprestas.ForEach(e => e.AcusarRecebimento());

            sut.EditarEmprestimo(emprestimo);

            sut.GetAllEmprestimos().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var emprestimoPersistido = sut.GetAllEmprestimos().First();
            emprestimo.Should().BeEquivalentTo(emprestimo, "este foi o funcionário adicionado");

            emprestimoPersistido.Finalizado.Should().BeTrue(" a única ferramenta alocada foi devolvida");

        }


        public void Dispose()
        {
            TestContext.Database.EnsureDeleted();
            TestContext.Dispose();
        }



    }




}

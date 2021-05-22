using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Tests;
using Almocherifado.InfraEstrutura.Repositorios;
using Almocherifado.InfraEstrutura.Tests.TestesEmprestimoRepositoy;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class FuncionariosRepositoryTests : IDisposable
    {
        AlmocherifadoContext testContext;
        public FuncionariosRepositoryTests()
        {

            DbContextOptions<AlmocherifadoContext> options = new 
                DbContextOptionsBuilder<AlmocherifadoContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=almocherifadoTests;Trusted_Connection=True;MultipleActiveResultSets=true")
           .Options; 

            testContext = new AlmocherifadoContext(options);
            testContext.Database.EnsureDeleted();
            testContext.Database.EnsureCreated();

        }
        
        [Theory, DomainAutoData]
        public void Com_Banco_Vazio_Conseguirmos_Adicionar_Funcionario(Funcionario funcionario)
        {   
             IFuncionariosRepository sut = new FuncionariosRepository(testContext);

            sut.AdicionarFuncionario(funcionario);

            sut.GetFuncionarios().Count().Should().BeGreaterThan(0,"Um funcionário foi adicionado na memmória") ;

            var funcionarioPersistido = sut.GetFuncionarios().Last();
            funcionarioPersistido.Should().BeEquivalentTo(funcionario, "este foi o funcionário adicionado");
            funcionarioPersistido.Nome.Should().BeEquivalentTo(funcionario.Nome, "este foi o nome do funcionário adicionado");
            funcionarioPersistido.Email.Should().BeEquivalentTo(funcionario.Email, "este foi o email do funcionário adicionado");
        }

        public void Dispose()
        {
            testContext.Database.EnsureDeleted();
            testContext.Dispose(); 
        }

        [Theory, DomainAutoData]
        public void Funcionario_Deve_Ser_Deletado(Funcionario funcionario1, Funcionario funcionario2)
        {
            IFuncionariosRepository sut = new FuncionariosRepository(testContext);
            var count = sut.GetFuncionarios().Count();

            sut.AdicionarFuncionario(funcionario1);
            sut.AdicionarFuncionario(funcionario2);

            sut.DeletarFuncionario(funcionario1);

            sut.GetFuncionarios().Count().Should().Be(++count,"haviam dois funcionários e um foi dleetado");

            sut.GetFuncionarios().Last().Should().Be(funcionario2);

        }

    }


}

using Almocherifado.core;
using Almocherifado.InfraEstrutura.Repositorios;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class FuncionariosRepositoryTests
    {
        

        [Fact]
        public void Com_Banco_Vazio_Conseguirmos_Adicionar_Funcionario_InMemory()
        {

            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>().UseInMemoryDatabase("test").Options;

            AlmocherifadoContext memoryContext = new AlmocherifadoContext(options);
            
             IFuncionariosRepository repository = new FuncionariosRepository(memoryContext);

            var funcionario = new Funcionario("Jessé Junior", "01724125109", "junior.jesse@gmail.com");

            repository.AdicionarFuncionario(funcionario);

            repository.GetFuncionarios().Count().Should().BeGreaterThan(0,"Um funcionário foi adicionado na memmória") ;

            var funcionarioPersistido = repository.GetFuncionarios().First();
            funcionarioPersistido.Should().BeEquivalentTo(funcionario, "este foi o funcionário adicionado");
            funcionarioPersistido.Nome.Should().BeEquivalentTo(funcionario.Nome, "este foi o nome do funcionário adicionado");
            funcionarioPersistido.Email.Should().BeEquivalentTo(funcionario.Email, "este foi o email do funcionário adicionado");
        }

        [Fact]
        public void Funcionario_Deve_Ser_Deletado()
        {

            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>().UseInMemoryDatabase("test").Options;

            BancoDeDadosComFuncioanrio memoryContext = new BancoDeDadosComFuncioanrio(options);
            memoryContext.Database.Migrate();

            IFuncionariosRepository repository = new FuncionariosRepository(memoryContext);

            var qtdInicial =  repository.GetFuncionarios().Count();

            repository.DeletarFuncionario(memoryContext.funcionarioSeed1);




        }

        class BancoDeDadosComFuncioanrio : AlmocherifadoContext
        {
            public Funcionario funcionarioSeed1 = new Funcionario("jessé jr", "01724125109", "junio.jesse@gmail.com");
            public Funcionario funcionarioSeed2 = new Funcionario("doria fulano", "666.927.308-80", "email@gmail.com");
            public BancoDeDadosComFuncioanrio(DbContextOptions<AlmocherifadoContext> optionsBuilder) : base(optionsBuilder)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Funcionario>().HasData(funcionarioSeed1,funcionarioSeed2);
            }
        }
    }


}

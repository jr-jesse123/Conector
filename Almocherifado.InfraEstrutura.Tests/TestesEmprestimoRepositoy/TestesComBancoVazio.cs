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

namespace Almocherifado.InfraEstrutura.Tests.TestesEmprestimoRepositoy
{
    public class TestesComBancoVazio : IDisposable
    {
        
        private Funcionario funcionario;
        private Ferramenta ferramenta;
        private Emprestimo emprestimo;
        private AlmocherifadoContext memoryContext;

        public TestesComBancoVazio()
        {
            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>()
                .UseSqlite(@$"Data Source = Testesalmocherifado.db;").Options;


            memoryContext = new AlmocherifadoContext(options);
            memoryContext.Database.EnsureDeleted();
            
            memoryContext.Database.Migrate();

            funcionario = new Funcionario("jessé Junior", "01724125109", "g@mgail.com");

            ferramenta = new Ferramenta("Ferramenta1", "ela é da boa", DateTime.Now.AddDays(-7), "/foto/url");

            emprestimo = new Emprestimo(DateTime.Now, funcionario, "Obra 1", ferramenta);
        }

   
        [Fact]
        public void Emprestimo_Eh_Adicionado_Com_Sucesso()
        {
            EmprestimosRepository repository = new EmprestimosRepository(memoryContext);
        //var ferramenta = new Ferramenta("Ferramenta1", "Pense numa ferramenta da boa", DateTime.Today.AddDays(-7), @"/fotos/foto");

            repository.SalvarNovoEmprestimo(emprestimo);

            repository.GetAllEmprestimos().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var emprestimoPersistido = repository.GetAllEmprestimos().First();
            emprestimo.Should().BeEquivalentTo(emprestimo, "este foi o funcionário adicionado");
        }
        
        [Fact]
        public void Emprestimo_Eh_Editado_Finalizado_E_Persistido_Com_Sucesso()
        {

            var repository = new EmprestimosRepository(memoryContext);
            repository.SalvarNovoEmprestimo(emprestimo);
            //var ferramenta = new Ferramenta("Ferramenta1", "Pense numa ferramenta da boa", DateTime.Today.AddDays(-7), @"/fotos/foto");

            emprestimo.Finalizado.Should().BeFalse(" O emprestimo ainda tem ferramentas alocadas");

            emprestimo.Ferramentas.First().AcusarRecebimento();

            repository.EditarEmprestimo(emprestimo);

            repository.GetAllEmprestimos().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var emprestimoPersistido = repository.GetAllEmprestimos().First();
            emprestimo.Should().BeEquivalentTo(emprestimo, "este foi o funcionário adicionado");

            emprestimoPersistido.Finalizado.Should().BeTrue(" a única ferramenta alocada foi devolvida");

        }

        [Fact]
        public void Emprestimo_Eh_Ddeletado_Com_Sucesso()
        {

            var repository = new EmprestimosRepository(memoryContext);
            repository.SalvarNovoEmprestimo(emprestimo);
            //var ferramenta = new Ferramenta("Ferramenta1", "Pense numa ferramenta da boa", DateTime.Today.AddDays(-7), @"/fotos/foto");
            

            emprestimo.Finalizado.Should().BeFalse(" O emprestimo ainda tem ferramentas alocadas");

            emprestimo.Ferramentas.First().AcusarRecebimento();

            repository.EditarEmprestimo(emprestimo);

            repository.GetAllEmprestimos().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var emprestimoPersistido = repository.GetAllEmprestimos().First();
            emprestimo.Should().BeEquivalentTo(emprestimo, "este foi o funcionário adicionado");

            emprestimoPersistido.Finalizado.Should().BeTrue(" a única ferramenta alocada foi devolvida");

        }
        public void Dispose()
        {
            memoryContext.Database.EnsureDeleted();
            memoryContext.Dispose();
        }


    }




}

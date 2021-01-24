using Almocherifado.core;
using Almocherifado.InfraEstrutura.Repositorios;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class FerramentasRepositoryTests
    {
        [Fact]
        public void Com_Banco_Vazio_Conseguirmos_Adicionar_Ferramenta_InMemory()
        {

            var caminho = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Teste.mdf");
            var connMaster = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Teste;Integrated Security=True");
            
            
            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>()
                .UseSqlServer(connMaster).Options;
            //Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = master; ; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False
            AlmocherifadoContext memoryContext = new AlmocherifadoContext(options);
            memoryContext.Database.EnsureDeleted();
            memoryContext.Database.EnsureCreated();

            IFerramentaRepository repository = new FerramentaRepository(memoryContext);

            var ferramenta = new Ferramenta("Ferramenta1","Pense numa ferramenta da boa", DateTime.Today.AddDays(-7),@"/fotos/foto");

            repository.AdicionarFerramenta(ferramenta);

            repository.GetallFerramentas().Count().Should().BeGreaterThan(0, "Um funcionário foi adicionado na memmória");

            var ferramentapersistida = repository.GetallFerramentas().First();
            ferramentapersistida.Should().BeEquivalentTo(ferramenta, "este foi o funcionário adicionado");
            
        }

        [Fact]
        public void Ferramenta_Deve_Ser_Deletado()
        {

            DbContextOptions<AlmocherifadoContext> options = new DbContextOptionsBuilder<AlmocherifadoContext>().UseInMemoryDatabase("test").Options;

            BancoDeDadosComFerramenta memoryContext = new BancoDeDadosComFerramenta(options);


            IFerramentaRepository repository = new FerramentaRepository(memoryContext);

            var qtdInicial = repository.GetallFerramentas().Count();

            repository.DeletarFerramenta(memoryContext.ferramentaSedd1);

            repository.GetallFerramentas().Count().Should().Be(1, "haviam dois funcionários e um foi dleetado");

        }

        class BancoDeDadosComFerramenta : AlmocherifadoContext
        {
            public Ferramenta ferramentaSedd1 = new Ferramenta("Ferramenta2", "FerramentaRuim1", DateTime.Today,"\\foto\\foto2");


            public BancoDeDadosComFerramenta(DbContextOptions<AlmocherifadoContext> optionsBuilder) : base(optionsBuilder)
            {
                this.Add(ferramentaSedd1);
                this.Add(new Ferramenta("Ferramenta1", "FerramentaBoa1", DateTime.Today,"\\foto\\foto"));

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}

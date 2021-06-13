using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.FSharp.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace InfraEstrutura
{
    public class AlmocharifadoContext : DbContext
    {
        
        public AlmocharifadoContext(DbContextOptions<AlmocharifadoContext> options) : base(options)
        {

        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ferramenta> Ferramentas { get; set; }
        public DbSet<Alocacao> Alocaoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ferramenta>().Property(x => x.Fotos)
                .HasConversion(x => x.Aggregate((a, b) => a + ";" + b), x => x.Split(new char[] { ';' }) );

            builder.Entity<Funcionario>().HasKey(f => f.CPF);
        }
    }
}

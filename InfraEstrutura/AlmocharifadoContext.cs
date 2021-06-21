using AutoFixture;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.FSharp.Collections;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace InfraEstrutura
{
    public class AlmocharifadoContext : DbContext
    {
     
        public AlmocharifadoContext(
            DbContextOptions<AlmocharifadoContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Devolucao> Devolucaos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ferramenta> Ferramentas { get; set; }
        public DbSet<Alocacao> Alocaoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ferramenta>().HasKey(f => f.Patrimonio);

            builder.Entity<Ferramenta>().Property(x => x.Fotos)
                .HasConversion(x => x.Aggregate((a, b) => a + ";" + b), x => x.Split(new char[] { ';' }) );

            builder.Entity<Funcionario>().HasKey(f => f.CPF);

            

            builder.Entity<Alocacao>().HasMany(a => a.Devolucoes); ;
            


        }

        

    }
}

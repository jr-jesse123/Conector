using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Tests;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Almocherifado.InfraEstrutura
{
    public class AlmocherifadoContext : DbContext, IAlmocherifadoContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ferramenta> Ferramentas { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        public AlmocherifadoContext(DbContextOptions<AlmocherifadoContext> optionsBuilder) : base(optionsBuilder)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>()
                .Property(f => f.CPF).HasConversion(p => p.Value, p => CPF.Create(p).Value);

            modelBuilder.Entity<Funcionario>()
                .HasKey(f => f.CPF);


            modelBuilder.Entity<Funcionario>()
          .Property(f => f.Email).HasConversion(p => p.Value, p => Email.Create(p).Value);

            modelBuilder.Entity<Funcionario>()
              .Property(f => f.Nome).HasConversion(p => p.Value, p => Nome.Create(p).Value);


            modelBuilder.Entity<Ferramenta>().HasKey(f => f.Id);
            modelBuilder.Entity<Ferramenta>().Property(f => f.NomeAbreviado);
            modelBuilder.Entity<Ferramenta>().Property(f => f.Descricao);
            modelBuilder.Entity<Ferramenta>().Property(f => f.DataCompra);
            modelBuilder.Entity<Ferramenta>().Property(f => f.FotoUrl);
            modelBuilder.Entity<Ferramenta>().Property(f => f.Marca);
            modelBuilder.Entity<Ferramenta>().Property(f => f.Modelo);
            modelBuilder.Entity<Ferramenta>().HasMany(f => f.HistoricoEmprestimos).WithOne(h => h.Ferramenta);
            

            modelBuilder.Entity<Emprestimo>().HasKey(e => e.Id);
            modelBuilder.Entity<Emprestimo>().HasOne(e => e.Funcionario).WithMany().IsRequired();
            modelBuilder.Entity<Emprestimo>().HasMany(e => e.FerramentasEmprestas).WithOne(fe => fe.Emprestimo)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            modelBuilder.Entity<Emprestimo>().Property(e => e.Entrega).IsRequired();
            modelBuilder.Entity<Emprestimo>().Property(e => e.Obra).IsRequired();
            modelBuilder.Entity<Emprestimo>().HasOne(e => e.Funcionario);
            modelBuilder.Entity<Emprestimo>().HasMany(e => e.FerramentasEmprestas).WithOne(f => f.Emprestimo);
            modelBuilder.Entity<Emprestimo>().Property(e => e.TermoResponsabilidade);


            modelBuilder.Entity<FerramentaEmprestada>().HasIndex(fe => fe.Id);
            modelBuilder.Entity<FerramentaEmprestada>().HasOne(fe => fe.Ferramenta).WithMany(f => f.HistoricoEmprestimos);
            modelBuilder.Entity<FerramentaEmprestada>().HasOne(fe => fe.Emprestimo).WithMany(e => e.FerramentasEmprestas);




            DomainFixture fixture = new();

            //TODO: REMOVER ESTE SEED JUNTO COM OS PRIVATE SETTERS
            var ferramentas = fixture.Build<Ferramenta>().CreateMany(50);
            var IdInfo = typeof(Ferramenta).GetProperty("Id");
            int lastId = 1;
            ferramentas.ToList().ForEach(f => IdInfo.SetValue(f, lastId++));

            var UrlInfo = typeof(Ferramenta).GetProperty(nameof(Ferramenta.FotoUrl));
            ferramentas.ToList().ForEach(f => UrlInfo.SetValue(f, "132564796661945613.jpg"));

            modelBuilder.Entity<Ferramenta>().HasData(ferramentas);
            modelBuilder.Entity<Funcionario>().HasData(fixture.CreateMany<Funcionario>(10));

            base.OnModelCreating(modelBuilder);
        }


    }

    public static class ExpressionHelper
    {
        public static Expression<Func<TEntity, TResult>> GetMember<TEntity, TResult>(String memberName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");
            MemberExpression member = Expression.MakeMemberAccess(parameter, typeof(TEntity).GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Single());
            Expression<Func<TEntity, TResult>> expression = Expression.Lambda<Func<TEntity, TResult>>(member, parameter);
            return (expression);
        }
     }
}

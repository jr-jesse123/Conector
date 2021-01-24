using Almocherifado.core;
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
            //modelBuilder.Entity<Funcionario>().Property (ExpressionHelper.GetMember<Funcionario,string>("_cpf"));
            //modelBuilder.Entity<Funcionario>().Property(f => f._cpf);
            //modelBuilder.Entity<Funcionario>().HasKey("_cpf");

            

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
            modelBuilder.Entity<Ferramenta>().Property(f => f.Descrição);
            modelBuilder.Entity<Ferramenta>().Property(f => f.DataCompra);
            modelBuilder.Entity<Ferramenta>().Property(f => f.FotoUrl);
            modelBuilder.Entity<Ferramenta>().HasMany(f => f.HistoricoEmprestimos).WithOne(e => e.Ferramenta);


            modelBuilder.Entity<Emprestimo>().Ignore(e => e.Ferramentas);
            modelBuilder.Entity<Emprestimo>().HasKey(e => e.Id);
            modelBuilder.Entity<Emprestimo>().HasOne(e => e.Funcionario).WithMany().IsRequired();
            //modelBuilder.Entity<Emprestimo>().HasOne(ExpressionHelper.GetMember<Emprestimo, FerramentaEmprestada>("ferramentas"));
            modelBuilder.Entity<Emprestimo>().HasMany("_ferramentas").WithOne();
            
            modelBuilder.Entity<Emprestimo>().Property(e => e.Entrega).IsRequired();
            modelBuilder.Entity<Emprestimo>().Property(e => e.Obra).IsRequired();


            modelBuilder.Entity<FerramentaEmprestada>().HasIndex(fe => fe.Id);
            modelBuilder.Entity<FerramentaEmprestada>().HasOne(fe => fe.Ferramenta).WithMany(f => f.HistoricoEmprestimos);
            modelBuilder.Entity<FerramentaEmprestada>().HasOne(fe => fe.Emprestimo).WithMany(f => f.Ferramentas);


            //modelBuilder.Entity<Ferramenta>().HasData();

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

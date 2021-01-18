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

        public AlmocherifadoContext(DbContextOptions<AlmocherifadoContext> optionsBuilder) : base(optionsBuilder)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>()
                .Property(f => f.CPF).HasConversion(p => p.Value, p => CPF.Create(p).Value);

            modelBuilder.Entity<Funcionario>().HasKey(f => f.CPF);


            modelBuilder.Entity<Funcionario>()
          .Property(f => f.Email).HasConversion(p => p.Value, p => Email.Create(p).Value);

            modelBuilder.Entity<Funcionario>()
              .Property(f => f.Nome).HasConversion(p => p.Value, p => Nome.Create(p).Value);

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

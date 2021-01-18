using Almocherifado.core;
using Microsoft.EntityFrameworkCore;

namespace Almocherifado.InfraEstrutura
{
    public interface IAlmocherifadoContext
    {
        DbSet<Funcionario> Funcionarios { get; set; }
        int SaveChanges();
    }
}
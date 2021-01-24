using Almocherifado.core;
using Microsoft.EntityFrameworkCore;

namespace Almocherifado.InfraEstrutura
{
    public interface IAlmocherifadoContext
    {
        DbSet<Funcionario> Funcionarios { get; set; }
        DbSet<Ferramenta> Ferramentas { get; set; }
        DbSet<Emprestimo> Emprestimos { get; set; }

        int SaveChanges();
    }
}
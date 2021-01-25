using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
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
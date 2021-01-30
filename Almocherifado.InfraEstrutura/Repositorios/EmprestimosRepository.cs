using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public class EmprestimosRepository : IEmprestimosRepository
    {
        private readonly AlmocherifadoContext context;

        public EmprestimosRepository(AlmocherifadoContext context)
        {
            this.context = context;
        }

        public void SalvarNovoEmprestimo(Emprestimo emprestimo)
        {
            if (context.Funcionarios.Contains(emprestimo.Funcionario))
            {
                var local = context.Set<Funcionario>().Local.Where(f => f == emprestimo.Funcionario).First();
                context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                context.Update(emprestimo.Funcionario);
            }

            foreach (var ferramenta in emprestimo.FerramentasEmprestas.Select(fe => fe.Ferramenta))
            {
                if (context.Ferramentas.Contains(ferramenta))
                {
                    var local = context.Set<Ferramenta>().Local.Where(f => f == ferramenta).First();
                    context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    context.Update(ferramenta);
                }
            }


            context.AttachRange(emprestimo.FerramentasEmprestas);

            context.Emprestimos.Add(emprestimo);
            context.SaveChanges();
        }


        public void EditarEmprestimo(Emprestimo emprestimo)
        {
            context.Emprestimos.Update(emprestimo);
            context.SaveChanges();
        }

        public List<Emprestimo> GetAllEmprestimos()
        {
            return context.Emprestimos.ToList();
        }

    }
}

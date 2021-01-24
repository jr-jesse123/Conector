
using Almocherifado.core;
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

using Almocherifado.core.Entitys;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public class FuncionariosRepository : IFuncionariosRepository
    {
        private readonly IAlmocherifadoContext context;

        public FuncionariosRepository(AlmocherifadoContext context)
        {
            this.context = context;
        }


        public void AdicionarFuncionario(Funcionario funcionario)
        {
            context.Funcionarios.Add(funcionario);
            context.SaveChanges();
        }

        public IEnumerable<Funcionario> GetFuncionarios()
        {
            return context.Funcionarios.ToList();
        }

        public void DeletarFuncionario(Funcionario funcionario)
        {
            context.Funcionarios.Remove(funcionario);
            context.SaveChanges();
        }

    }
}

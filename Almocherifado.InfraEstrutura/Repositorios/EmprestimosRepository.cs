using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public class EmprestimosRepository
    {
        private readonly AlmocherifadoContext context;

        public EmprestimosRepository(AlmocherifadoContext context)
        {
            this.context = context;
        }
    }
}

using Almocherifado.core.AgregateRoots.EmprestimoNm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.core.Commom
{
    public class EmprestimoCriadoEvent : IDomainEvent
    {
        public EmprestimoCriadoEvent(Emprestimo emprestimo)
        {
            Emprestimo = emprestimo;
        }

        public Emprestimo Emprestimo { get; }
    }
}

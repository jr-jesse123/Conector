using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.Commom;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.core.Tests
{
    public class DomainEventTests 
    {
        [Theory, DomainAutoData]
        public void CriarEmprestimoAtivaUmEvento(Emprestimo emprestimo)
        {
            var emprestimoCriadoEvent = emprestimo.DomainEvents[0] as EmprestimoCriadoEvent;
            emprestimoCriadoEvent.Should().NotBeNull();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.core.Commom
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void HandleAsync(T DomainEvent);
    }
}

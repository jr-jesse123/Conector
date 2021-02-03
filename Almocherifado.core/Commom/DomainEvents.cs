using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.core.Commom
{
    public static class DomainEvents
    {
        private static List<Type> handlers;

        static DomainEvents()
        {
            handlers = AppDomain.CurrentDomain.GetAssemblies()
                    .Select(a => a.GetTypes().Where(x => x.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>))
                )
                .ToList()).Aggregate((lista1,lista2) => 
                {
                    lista1.AddRange(lista2);
                    return lista1;
                } );
        }

        public static void Dispatch(IDomainEvent domainEvent)
        {
            foreach (Type   handlerType in handlers)
            {
                bool canHandle = handlerType.GetInterfaces()
                    .Any(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(IHandler<>)
                    && i.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandle)
                {
                    dynamic handler = Activator.CreateInstance(handlerType);
                    handler.HandleAsync((dynamic)domainEvent);
                }

            }
        }
    }
}

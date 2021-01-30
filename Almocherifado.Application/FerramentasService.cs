using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.InfraEstrutura;
using Almocherifado.InfraEstrutura.Repositorios;
using CSharpFunctionalExtensions;
using System;
using System.Linq;

namespace Almocherifado.Application
{
    public class FerramentasService : IFerramentasService
    {
        public AlmocherifadoContext Context { get; }
        
        public FerramentasService(AlmocherifadoContext context)
        {
            Context = context;
        }


        public bool VerificarSeFerramentaEstaEmprestada(Ferramenta ferramenta)
        {

            var ferramentasemprestadas = Context.Set<FerramentaEmprestada>().ToList();

            var emprestimoEmAndamento = Context.Set<FerramentaEmprestada>()
                .Where(fe => fe.Ferramenta == ferramenta)
                .Where(fe => !fe.DataDevolucao.HasValue).SingleOrDefault();

            return   
                 emprestimoEmAndamento is null ? false : true;
        }

        public void ReceberFerramenta(FerramentaEmprestada ferramentaEmprestada)
        {
            ferramentaEmprestada.AcusarRecebimento();

            //var local = Context.Set<FerramentaEmprestada>().Local.Where(fe => fe == ferramentaEmprestada).First();

            //if (local != null)
            //    Context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            //Context.Attach(ferramentaEmprestada);

            Context.SaveChanges();
        }


    }
}

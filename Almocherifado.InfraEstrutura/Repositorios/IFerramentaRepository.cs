using Almocherifado.core;
using System.Collections.Generic;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public interface IFerramentaRepository
    {
        void AdicionarFerramenta(Ferramenta ferramenta);
        void DeletarFerramenta(Ferramenta ferramenta);
        IEnumerable<Ferramenta> GetallFerramentas();
    }
}
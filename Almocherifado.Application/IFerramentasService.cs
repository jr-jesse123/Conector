using Almocherifado.core.AgregateRoots.FerramentaNm;

namespace Almocherifado.Application
{
    public interface IFerramentasService
    {
        bool VerificarSeFerramentaEstaEmprestada(Ferramenta ferramenta);
    }
}
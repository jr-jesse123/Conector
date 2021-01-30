using Almocherifado.core.AgregateRoots.FerramentaNm;

namespace Almocherifado.Application
{
    public interface IFerramentasService
    {
        void ReceberFerramenta(FerramentaEmprestada ferramentaEmprestada);
        bool VerificarSeFerramentaEstaEmprestada(Ferramenta ferramenta);
    }
}
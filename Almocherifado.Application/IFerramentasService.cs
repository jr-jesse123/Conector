using Almocherifado.core;

namespace Almocherifado.Application
{
    public interface IFerramentasService
    {
        bool VerificarSeFerramentaEstaEmprestada(Ferramenta ferramenta);
    }
}
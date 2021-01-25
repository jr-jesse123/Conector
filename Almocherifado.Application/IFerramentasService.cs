using Almocherifado.core.Entitys;

namespace Almocherifado.Application
{
    public interface IFerramentasService
    {
        bool VerificarSeFerramentaEstaEmprestada(Ferramenta ferramenta);
    }
}
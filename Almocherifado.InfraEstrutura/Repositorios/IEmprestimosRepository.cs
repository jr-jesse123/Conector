using Almocherifado.core.Entitys;
using System.Collections.Generic;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public interface IEmprestimosRepository
    {
        void EditarEmprestimo(Emprestimo emprestimo);
        List<Emprestimo> GetAllEmprestimos();
        void SalvarNovoEmprestimo(Emprestimo emprestimo);
    }
}
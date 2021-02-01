using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Almocherifado.Application
{
    public interface ITermoManager
    {
        Task<Result<string>> BuildTermo(DateTime DataEntrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra);
    }
}
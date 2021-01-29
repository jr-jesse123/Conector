using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Almocherifado.ServerHosted.Services
{
    public interface ITermoManager
    {
        Result<string> BuildTermo(DateTime DataEntrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra);
    }
}
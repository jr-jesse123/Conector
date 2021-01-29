using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using System;
using System.Collections.Generic;
using Xceed.Words.NET;

namespace Almocherifado.core.Services
{
    public interface ITermoResponsabilidadeService
    {
        DocX GetTermoPreenchido(DateTime DataEntrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra);
    }
}
﻿using Almocherifado.core;
using System.Collections.Generic;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public interface IFuncionariosRepository
    {
        void AdicionarFuncionario(Funcionario funcionario);
        void DeletarFuncionario(Funcionario funcionario);
        IEnumerable<Funcionario> GetFuncionarios();
    }
}
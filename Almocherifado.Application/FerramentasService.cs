using Almocherifado.core;
using Almocherifado.InfraEstrutura;
using Almocherifado.InfraEstrutura.Repositorios;
using CSharpFunctionalExtensions;
using System;

namespace Almocherifado.Application
{
    public class FerramentasService
    {
        private readonly IFerramentaRepository repo;

        public FerramentasService(IFerramentaRepository context)
        {
            this.repo = context;
        }

        public bool VerificarSeFerramentaEstaEmprestada(Ferramenta ferramenta)
        {
            return repo.Procurar(ferramenta).HasValue ? true: false;
        }
        
    }
}

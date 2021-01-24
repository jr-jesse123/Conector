﻿using Almocherifado.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.InfraEstrutura.Repositorios
{
    public class FerramentaRepository : IFerramentaRepository
    {
        private readonly AlmocherifadoContext context;

        public FerramentaRepository(AlmocherifadoContext context)
        {
            this.context = context;
        }

        public void AdicionarFerramenta(Ferramenta ferramenta)
        {
            context.Ferramentas.Add(ferramenta);
            context.SaveChanges();
        }

        public IEnumerable<Ferramenta> GetallFerramentas()
        {
            return context.Ferramentas.AsNoTracking().ToList();
        }

        public void DeletarFerramenta(Ferramenta ferramenta)
        {
            context.Ferramentas.Remove(ferramenta);
            context.SaveChanges();
        }



    }
}
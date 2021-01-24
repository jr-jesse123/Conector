﻿using Almocherifado.core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Data.Models.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<FerramentaModel, Ferramenta>();
        }
    }
}
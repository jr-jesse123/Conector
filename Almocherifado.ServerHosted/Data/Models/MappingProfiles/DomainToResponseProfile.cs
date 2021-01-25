﻿using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
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
            CreateMap<FerramentaModel, Ferramenta>()
                .ForMember(dest=> dest.HistoricoEmprestimos, opt => opt.Ignore());
            CreateMap< Ferramenta, FerramentaModel>();

            CreateMap<FuncionarioModel, Funcionario>();
            

        }
    }
}

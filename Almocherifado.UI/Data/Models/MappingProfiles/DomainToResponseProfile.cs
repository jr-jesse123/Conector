using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.UI.Data.Models.MappingProfiles
{
    
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<FerramentaModel, Ferramenta>()
                .ForMember(dest => dest.HistoricoEmprestimos, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Ferramenta, FerramentaModel>();

            CreateMap<FuncionarioModel, Funcionario>()
                .ForMember(F => F.Id, OPT => OPT.Ignore())
                    .ForMember(F => F.CPF, OPT => OPT.MapFrom(f => f.CPF))
                ;

            CreateMap<Funcionario, FuncionarioModel>()
                ;


            CreateMap<EmprestimoModel, Emprestimo>()
                .ConstructUsing(mod => new Emprestimo(mod.entrega,
                new Funcionario(mod.Funcionario.Nome, mod.Funcionario.CPF, mod.Funcionario.Email),
                mod.Obra, mod.Ferramentas.ToArray()))
                .ForMember(e => e.FerramentasEmprestas, opt => opt.Ignore())
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.DomainEvents, opt => opt.Ignore())

                ;
        }
    }
}

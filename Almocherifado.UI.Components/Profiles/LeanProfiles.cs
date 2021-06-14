using Almocherifado.UI.Components.Helpers;
using Almocherifado.UI.Components.Models;
using AutoMapper;
using Entities;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Profiles
{
    public class LeanProfile  : Profile
    {
        public LeanProfile()
        {


            CreateMap<CadastroFerramentaModel, Ferramenta>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.DataCompra, (opt) => opt.MapFrom(cfm => cfm.DataDaCompra))
                .ForMember(dst => dst.Fotos, opt => opt.MapFrom((cfm, fe) => FileHelper.getFotoFerramentaPath(cfm).ToArray()));
                
        }
    }

}

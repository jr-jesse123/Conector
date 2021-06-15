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


            CreateMap<CadastroFuncionarioModel, Funcionario>()
                .ConvertUsing<CadastroFuncionarioConverter>();
                


            CreateMap<CadastroFerramentaModel, Ferramenta>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.DataCompra, (opt) => opt.MapFrom(cfm => cfm.DataDaCompra))
                .ForMember(dst => dst.EmManutencao, opt => opt.Ignore() )
                .ForMember(dst => dst.Fotos, opt => 
                           opt.MapFrom((cfm, fe) => FileHelper.getFotoFerramentaPath(cfm).ToArray())
                           );
        }
    }

    internal class CadastroFuncionarioConverter : ITypeConverter<CadastroFuncionarioModel, Funcionario>
    {
        public Funcionario Convert(CadastroFuncionarioModel source, Funcionario destination, ResolutionContext context)
        {
            return new Funcionario()
            {
                Nome = source.Nome,
                Cargo = source.Cargo,
                CPF = source.CPF,
                Email = source.Email,
                Foto = FileHelper.getFotFuncionarioPath(source)
            };

        }
    }

}

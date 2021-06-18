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
            CreateMap<DevolucaoInputModel, Devolucao[]>()
                .ConvertUsing<DevolucaoInputModelConverter>();

            CreateMap<CadastroAlocacaoModel, Entities.Alocacao>()
                .ForMember(dst => dst.DataAlocacao, opt => opt.MapFrom(cam => cam.Data))
                .ForMember(dst => dst.Devolucoes, opt => opt.MapFrom((x) => Array.Empty<Devolucao>()))
                .ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<CadastroFuncionarioModel, Funcionario>()
                .ConvertUsing<CadastroFuncionarioConverter>();

            CreateMap<CadastroFerramentaModel, Ferramenta>()
                .ConvertUsing<CadastroFerramentaConverter>();

        }
    }



    public class DevolucaoInputModelConverter : ITypeConverter<DevolucaoInputModel, Devolucao[]>
    {
        public Devolucao[] Convert(DevolucaoInputModel source, Devolucao[] destination, ResolutionContext context)
        {
           return  source.FerramentasEComentarios
                .Select(fec => new Devolucao(0, fec.Key, DateTime.Now, fec.Value))
                .ToArray();
        }
    }


    public class CadastroFerramentaConverter : ITypeConverter<CadastroFerramentaModel, Ferramenta>
    {
        public Ferramenta Convert(CadastroFerramentaModel source, Ferramenta destination, ResolutionContext context)
        {
            string[] fotos;
            try
            {
                fotos  = source.Fotos.Select(ft => $"{source.Patrimonio}_{ft.FileInfo.Name}").ToArray();
            }
            catch (NullReferenceException)
            {
                fotos = new string[] { };
            }
            
            return new Ferramenta()
            {
                Nome = source.Nome,
                Marca = source.Marca,
                Modelo = source.Modelo,
                DataCompra = source.DataDaCompra,
                Patrimonio = source.Patrimonio,
                Fotos = fotos,
                Descricao = source.Descricao
            };
        }
    }

    public class CadastroFuncionarioConverter : ITypeConverter<CadastroFuncionarioModel, Funcionario>
    {
        public Funcionario Convert(CadastroFuncionarioModel source, Funcionario destination, ResolutionContext context)
        {
            var foto = source.Foto?.FileInfo?.Name is null ? "" : $"{source.CPF}_{ source.Foto.FileInfo.Name}";
            return new Funcionario()
            {
                Nome = source.Nome,
                Cargo = source.Cargo,
                CPF = source.CPF,
                Email = source.Email,
                Foto = foto
            };

        }
    }

}

using Almocharifado.InfraEstrutura;
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
            //CreateMap<DevolucaoInputModel, Devolucao[]>()
            //    .ConvertUsing<DevolucaoInputModelConverter>();

            CreateMap<CadastroAlocacaoModel, AlocacaoInsert>()
                .ForMember(dst => dst.DataAlocacao, opt => opt.MapFrom(cam => cam.Data));
                //.ForMember(dst => dst.Devolucoes, opt => opt.MapFrom((x) => Array.Empty<Devolucao>()))
                //.ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<CadastroFuncionarioModel, FuncionarioInsert>()
                .ConvertUsing<CadastroFuncionarioConverter>();

            CreateMap<CadastroFerramentaModel, FerramentaInsert>()
                .ConvertUsing<CadastroFerramentaConverter>();

        }
    }



    //public class DevolucaoInputModelConverter : ITypeConverter<DevolucaoInputModel, Devolucao[]>
    //{
    //    public Devolucao[] Convert(DevolucaoInputModel source, Devolucao[] destination, ResolutionContext context)
    //    {
    //       return  source.FerramentasEComentarios
    //            .Select(fec => new Devolucao(0, fec.Key, DateTime.Now, fec.Value))
    //            .ToArray();
    //    }
    //}


    public class CadastroFerramentaConverter : ITypeConverter<CadastroFerramentaModel, FerramentaInsert>
    {
        public FerramentaInsert Convert(CadastroFerramentaModel source, FerramentaInsert destination, ResolutionContext context)
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
            
            return new FerramentaInsert()
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

    public class CadastroFuncionarioConverter : ITypeConverter<CadastroFuncionarioModel, FuncionarioInsert>
    {
        public FuncionarioInsert Convert(CadastroFuncionarioModel source, FuncionarioInsert destination, ResolutionContext context)
        {
            var foto = source.Foto?.FileInfo?.Name is null ? "" : $"{source.CPF}_{ source.Foto.FileInfo.Name}";
            return new FuncionarioInsert()
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

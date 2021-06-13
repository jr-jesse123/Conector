using Almocherifado.UI.Components.Models;
using Entities;
using InfraEstrutura;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlmocharifadoApplication;
using System;
using Microsoft.FSharp.Core;

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class  CadastrarFerramentas
    {
        private void OnValidInput()
        {
            formClass = "was-validated d-flex";
        }

        int ProximoPatrimoonio { get; set; }

        List<Ferramenta> Ferramentas;
        [Inject]AlmocharifadoContext context { get; set; }
        private string formClass = "d-flex";

        protected override void OnAfterRender(bool firstRender)
        {
            Ferramentas = context.Ferramentas.ToList();
            Func<Unit,IEnumerable<Ferramenta>> _fetcher = _ => Ferramentas;
            var fetcher = FSharpFunc<Unit, IEnumerable<Ferramenta>>
                .FromConverter(new Converter<Unit, IEnumerable<Ferramenta>>(_fetcher));

            ProximoPatrimoonio = Alocacoes.GetProximoPatrimonio(fetcher);
            StateHasChanged();
            //TODO: CONTINUAR DAQUI OBTENDO O PRÓXIMO NÚMERO DE PATRIMÔNIO DISPONÍVEEL
        }

        private string getPath (CadastroFerramentaModel FerramentaInput, string fileName) 
        { 
            return  @"./wwwroot/" + FerramentaInput.Nome + "_" + FerramentaInput.Marca
                        + "_" + FerramentaInput.Modelo + "_" + fileName + ".png";
        }

        private List<MemoryStream> fotos { get; set; } = new List<MemoryStream>();

        private void OnChange(UploadChangeEventArgs args)
        {
            fotos = new List<MemoryStream>();
            foreach (var file in args.Files)
            {
                var ms = new MemoryStream();
                file.Stream.WriteTo(ms);
                fotos.Add(ms);                
            }
        }
        public CadastroFerramentaModel FerramentaInput { get; set; } = new();
        //private void OnRemove(RemovingEventArgs args)
        //{
        //    foreach (var removeFile in args.FilesData)
        //    {
        //        //if (File.Exists(Path.Combine(@"rootPath", removeFile.Name)))
        //        //{
        //        //    File.Delete(Path.Combine(@"rootPath", removeFile.Name));
        //        //}
        //        //if (FerramentaInput.Fotos.Exists())
        //        //{

        //        //}
        //    }
        //}
    }
}

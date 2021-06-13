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
using Dtos;

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class CadastrarFerramentas
    {
        private void OnValidInput()
        {   
            //TODO: TESTAR ALTERAÇÃO DE ESTADO
            formClass = "was-validated d-flex";
        }
        
        private string formClass = "d-flex";
        int ProximoPatrimonio { get; set; }
        public FerramentaDto FerramentaInput { get; private set; } = new();
        [Parameter] public RenderFragment Uploader { get; set; }

        [Inject] IProximoPatrimonioProvider patrimonioProvider {get;set;}
        [Inject] IAlmocharifadoRepository repo { get; set; }
        [Inject] NavigationManager navMan { get; set; }
        

        protected override Task OnInitializedAsync()
        {
            ProximoPatrimonio = patrimonioProvider.GetProximoPatrimonio();
            FerramentaInput.Patrimonio = ProximoPatrimonio;
            return Task.CompletedTask;
        }
    }
}

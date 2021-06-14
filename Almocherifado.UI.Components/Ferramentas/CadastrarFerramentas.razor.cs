using Almocherifado.UI.Components.Models;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlmocharifadoApplication;
using Almocherifado.UI.Components.Helpers;
using System.Linq;
using AutoMapper;
using Entities;
using System;

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class CadastrarFerramentas
    {
        [Parameter] public RenderFragment Uploader { get; set; }
        [Inject] NavigationManager navMan { get; set; }
        [Inject] IMapper mapper { get; set; }
        [Inject] IProximoPatrimonioProvider patrimonioProvider { get; set; }
        [Inject] IAlmocharifadoRepository repo { get; set; }

        
        public void ChangeFotos(IEnumerable<UploadFiles> novasFotos)
        {
            FerramentaInput.Fotos = novasFotos;
        }

        private void OnSubmit()
        {
            formClass = "was-validated d-flex";
            if (form.EditContext.Validate())
            {
                OnValidSubmit();
            }
            OnInValidSubmit();
        }

        private void OnInValidSubmit()
        {
          
        }
        private void OnValidSubmit()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            //var ferramentaDomain = mapper.Map<Ferramenta>(FerramentaInput);

            var ferramentaDomain = new Ferramenta()
            {
                DataCompra = FerramentaInput.DataDaCompra,
                Descricao = FerramentaInput.Descricao,
                Fotos = FileHelper.getFotoFerramentaPath(FerramentaInput).ToArray(),
                Marca = FerramentaInput.Marca,
                Modelo = FerramentaInput.Modelo,
                Nome = FerramentaInput.Nome,
                Patrimonio = FerramentaInput.Patrimonio
            };

            repo.SalvarFerramenta(ferramentaDomain);
        }
        
        private string formClass = "d-flex";
        int ProximoPatrimonio { get; set; }
        public CadastroFerramentaModel FerramentaInput { get; private set; } = new() { DataDaCompra = DateTime.Today.AddDays(1)};

        protected override Task OnInitializedAsync()
        {
            ProximoPatrimonio = patrimonioProvider.GetProximoPatrimonio();
            FerramentaInput.Patrimonio = ProximoPatrimonio;
            return Task.CompletedTask;
        }
    }
}

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
using Microsoft.AspNetCore.Components.Forms;

namespace Almocherifado.UI.Components.Forms
{
    public abstract class FormBase : ComponentBase
    {

        [Inject] protected NavigationManager navMan { get; set; }
        [Inject] protected IAlmocharifadoRepository repo { get; set; }
        [Inject] protected IMapper mapper { get; set; }



        [Parameter] public RenderFragment Uploader { get; set; }

        protected EditForm form;

        protected virtual void OnSubmit() 
        {
            formClass = "was-validated d-flex";
            //form.EditContext.Validate();
        }

        protected string formClass = "d-flex";
        protected void OnCancelClic()
        {
            navMan.NavigateTo("/");
        }

    }
}

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class CadastrarFerramentas
    {
        [Parameter] public RenderFragment Uploader { get; set; }
        [Inject] NavigationManager navMan { get; set; }
        [Inject] IMapper mapper { get; set; }
        [Inject] IProximoPatrimonioProvider patrimonioProvider { get; set; }
        [Inject] IAlmocharifadoRepository repo { get; set; }


        public EditForm form { get; private set; }
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
        }

        private void OnCancelClic() 
        {
            navMan.NavigateTo("/");
        }

        private void OnValidSubmit()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var ferramentaDomain = mapper.Map<Ferramenta>(FerramentaInput);


            FileHelper.SaveFilesToRoot(FerramentaInput.Fotos, ferramentaDomain.Fotos);

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

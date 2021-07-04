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
using Almocharifado.InfraEstrutura;

namespace Almocherifado.UI.Components.Forms
{
}

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class CadastrarFerramentas
    {
        
        [Parameter] public RenderFragment Uploader { get; set; }
        [Inject] NavigationManager navMan { get; set; }
        [Inject] IMapper mapper { get; set; }
        //[Inject] IProximoPatrimonioProvider patrimonioProvider { get; set; }
        [Inject] IAlmocharifadoRepository repo { get; set; }
        [Inject] IFerramentaRepository ferraemntaRepo { get; set; }


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
                OnValidSubmitAsync();
            }
        }

        private void OnCancelClic() 
        {
            navMan.NavigateTo("/");
        }

        private async void OnValidSubmitAsync()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var ferramentaDomain = mapper.Map<FerramentaInsert>(FerramentaInput);

            for (int i = 0; i < FerramentaInput.Fotos.Count(); i++)
            {
                FileHelper.SaveFileToRoot(FerramentaInput.Fotos.ElementAt(i).Stream, ferramentaDomain.Fotos[i]);
            }
            

            try
            {
                //TODO: LIDAR COM CONCORRÊNCIA DO PATRIMÔNIO.. O QUE ACONTECE SE DUAS PESSOAS ESTIVEREM CADASTRANDOA O MESMO TEMPO?
                ferraemntaRepo.SalvarFerramenta(ferramentaDomain);
                toast.Show("Ferramenta cadastrada corretamente", "sucesso");
                var proxPatri = ferraemntaRepo.GetAllFerramentas().Max(fe => fe.Patrimonio) + 1;

                if (proxPatri == ProximoPatrimonio)
                {
                    throw new Exception("falha na atualização de patrimônio apóos a inclusão de ferramenta");
                }

                ProximoPatrimonio = ferraemntaRepo.GetAllFerramentas().Max(fer => fer.Patrimonio) + 1;
                FerramentaInput = new() 
                {
                    DataDaCompra = DateTime.Today.AddDays(1) , 
                    Patrimonio= ProximoPatrimonio
                };
                
                formClass= formClass.Replace("was-validated", "");
                 
            }
            catch (Exception ex)
            {
                toast.Show(ex.Message, "ERRO");
            }
        }

        Toast toast;

        private string formClass = "d-flex";
        int ProximoPatrimonio { get; set; }
        public CadastroFerramentaModel FerramentaInput { get; private set; } = new() { DataDaCompra = DateTime.Today.AddDays(1)};

        protected override Task OnInitializedAsync()
        {
            ProximoPatrimonio = ferraemntaRepo.GetAllFerramentas().Max(fer => fer.Patrimonio) + 1;
            FerramentaInput.Patrimonio = ProximoPatrimonio;
            return Task.CompletedTask;
        }
    }
}

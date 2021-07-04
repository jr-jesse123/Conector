using Microsoft.AspNetCore.Components;
using AlmocharifadoApplication;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Almocharifado.InfraEstrutura;

namespace Almocherifado.UI.Components.Forms
{
    public abstract class FormBase : ComponentBase
    {
        [Inject] protected NavigationManager navMan { get; set; }
        [Inject] protected IAlmocharifadoRepository repo { get; set; }
        [Inject] protected IFerramentaRepository ferramentaRepo { get; set; }
        [Inject] protected IMapper mapper { get; set; }

        [Parameter] public RenderFragment Uploader { get; set; }

        public EditForm form;

        protected virtual void OnSubmitAsync() 
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

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components
{
    public partial class Modal
    {
        public void Hide()
        {
            modalclass = "modal fade";
            StateHasChanged();
        }
        public void Show()
        {
            modalclass = "modal fade show";
        }

        string modalclass { get; set; } = "modal fade";
        [Parameter] public RenderFragment body { get; set; }

        [Parameter] public string TextoConfirma { get; set; } = "Save Changes";
        [Parameter] public string TextoTitulo { get; set; } = "Titulo";
        [Parameter] public EventCallback OnCorfirma { get; set; }
        async Task Confirmar()
        {
            await OnCorfirma.InvokeAsync();

            Hide();
            StateHasChanged();
        }
    }
}

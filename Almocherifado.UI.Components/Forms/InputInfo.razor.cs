using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Forms
{
    public partial class InputInfo
    {

        string classe;

        protected override void OnParametersSet()
        {

            classe = Tipo switch
            {
                InputInfoType.Valid => "valid-feedback",
                InputInfoType.Invalid => "invalid-feedback",
                InputInfoType.Disclaimer => "form-text",
                _ => "form-text"
            };

        }

        [Parameter] public string Text { get; set; }
        [Parameter] public InputInfoType Tipo { get; set; }
    }
}

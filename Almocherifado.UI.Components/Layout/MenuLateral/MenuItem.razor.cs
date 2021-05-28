using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Layout.MenuLateral
{
    public partial class MenuItem
    {
        //TODO: INVERTER A SETA QUANDO EXPANDIDO


        string collapseClass;
        string arrowDirectionClass = "";

        protected override void OnParametersSet()
        {
            collapseClass = isCollapsed ? "collapse" : "collapse show";
            arrowDirectionClass = isCollapsed ? "" : "inverted";
        }

        public void Toogle()
        {
            isCollapsed = !isCollapsed;

            OnParametersSet();
        }

        [Parameter] public bool isCollapsed { get; set; } = true;
        [Parameter] public string Titulo { get; set; } = "";
        [Parameter] public List<(string Text, string Href)> Links { get; set; } = new ();
        [Parameter] public string Selected { get; set; } = "";
        [Parameter] public RenderFragment Icon { get; set; }

    }
}

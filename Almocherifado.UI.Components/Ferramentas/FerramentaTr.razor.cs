using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class FerramentaTr
    {
        [Parameter] public Ferramenta Ferramenta {get;set;} 
        [Parameter] public RenderFragment StatusFragment { get; set; }

    }
}

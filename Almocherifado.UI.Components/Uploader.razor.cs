using Almocherifado.UI.Components.Helpers;

using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components
{
    public partial class Uploader
    {
        [Parameter] public string[] PathFotos { get; set; }
        [Parameter] public EventCallback<IEnumerable<UploadFiles>> FotosChanged { get; set; }

        public IEnumerable<UploadFiles> Files { get; private set; } = new UploadFiles[] { };
        private void OnChange(UploadChangeEventArgs args)
        {
            Files = args.Files;
            FotosChanged.InvokeAsync(Files).Wait();   
        }
    }
}

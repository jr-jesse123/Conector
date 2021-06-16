using Almocherifado.UI.Components.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        [Parameter] public bool Multiple { get; set; } = true;
        [Parameter] public string[] PathFotos { get; set; }
        //[Parameter] public EventCallback<IEnumerable<IBrowserFile>> FotosChanged { get; set; }
        [Parameter] public EventCallback<IEnumerable<UploadFiles>> FotosChanged { get; set; }

        //public IEnumerable<IBrowserFile> Files { get; private set; } = new List<IBrowserFile>();
        public IEnumerable<UploadFiles> Files { get; private set; } = new List<UploadFiles>();
        private void OnChange(UploadChangeEventArgs args)
        {
            Files = args.Files;
            FotosChanged.InvokeAsync(Files).Wait();
        }

        //TODO: CONSTRUIR OUTRO FILE UPLOADER
        //private void LoadFiles(InputFileChangeEventArgs e)
        //{
        //    var stream = e.File.OpenReadStream();

        //    var arquivostr =  new StreamReader(stream).ReadToEnd();

        //}
    }
}

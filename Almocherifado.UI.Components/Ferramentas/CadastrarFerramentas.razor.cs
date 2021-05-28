using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Ferramentas
{
    public partial class  CadastrarFerramentas
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        [Inject] IJSRuntime jSRuntime { get; set; }

   
        public CadastrarFerramentas()
        {
            moduleTask = new(() => jSRuntime
            .InvokeAsync<IJSObjectReference>("import",
                "./_content/Almocherifado.UI.Components/InputFile.js").AsTask());
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("AjustarInput");
            await module.InvokeVoidAsync("IniciarPopOvers");
            base.OnAfterRender(firstRender);
        }


        private void OnChange(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var path = @"path" + file.FileInfo.Name;
                FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.Write);
                file.Stream.WriteTo(filestream);
                filestream.Close();
                file.Stream.Close();
            }
        }
    }
}

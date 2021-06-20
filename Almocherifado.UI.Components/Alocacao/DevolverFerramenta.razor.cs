using AlmocharifadoApplication;
using Almocherifado.UI.Components.Forms;
using Almocherifado.UI.Components.Models;
using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Alocacao
{
    public partial class DevolverFerramenta : FormBase
    {

        [Inject] IAlmocharifadoRepository repo { get; set; }
        [Inject] IFerramentaRepository ferramentaRepo { get; set; }
        DevolucaoInputModel devolucaoInput { get; set; } = new()  ;


        protected override void OnSubmitAsync()
        {
            base.OnSubmitAsync();

            devolucaoInput.FerramentasEComentarios = FerramentasEComentarios;

            if (form.EditContext.Validate())
            {
                var devolucoes = mapper.Map<Devolucao[]>(devolucaoInput);

                ferramentaRepo.DevolverFerramentas(devolucoes);
                devolucaoInput = new();
                FerramentasEComentarios = new();
            } 

        }
        Dictionary<Ferramenta,string> FerramentasEComentarios { get; set; } = new();
    }
}

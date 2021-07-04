using Almocharifado.InfraEstrutura;
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
        DevolucaoInputModel devolucaoInput { get; set; } = new();
        
        Entities.Alocacao[] Alocacoes = new Entities.Alocacao[] { };

        protected override void OnInitialized()
        {
            Alocacoes = repo.GetAllAlocacoes();
        }
        protected override void OnSubmitAsync()
        {
            base.OnSubmitAsync();

            devolucaoInput.FerramentasEComentarios = FerramentasEComentarios;

            if (form.EditContext.Validate())
            {
                

                foreach (var devolucaoInput in devolucaoInput.FerramentasEComentarios)
                {
                    var ferramenta = devolucaoInput.Key;
                    var alocacao = Alocacoes.Where(aloc =>
                        aloc.Finalizada == false &&
                        aloc.FerramentasAlocadas
                            .Where(fa => !fa.Devolvida)
                            .Select(fa => fa.Ferramenta).Contains(ferramenta))
                            .Single();

                    ferramentaRepo.RegistrarDevolucaoDeDevolverFerramenta(alocacao,ferramenta,DateTime.Now,devolucaoInput.Value);
                }
                



                
                devolucaoInput = new();
                FerramentasEComentarios = new();
            } 

        }
        Dictionary<Ferramenta,string> FerramentasEComentarios { get; set; } = new();
    }
}

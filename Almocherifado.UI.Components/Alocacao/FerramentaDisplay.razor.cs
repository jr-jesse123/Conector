using AlmocharifadoApplication;
using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.FSharp.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Alocacao
{
    public partial class FerramentaDisplay
    {
        [Inject] IFerramentaRepository ferramentaRepository { get; set; }
        [Inject] IAlmocharifadoRepository Repositorio { get; set; }
        Ferramenta[] Ferramentas;
        Entities.Alocacao[] Alocacoes;
        Ferramenta ferramentaAtual { get; set; }
        bool FeramentaDisponivel 
        {
            get 
            {
                if (ferramentaAtual is not null)
                {
                    return Entities.Ferramentas.FerramentaDisponivel(Alocacoes, ferramentaAtual);
                }
                else
                {
                    return false;
                }
            }
        }

        [Parameter] public EventCallback<Ferramenta> OnFerramentaSelecionada { get; set; }

        async Task FerramentaSetada()
        {
            await OnFerramentaSelecionada.InvokeAsync(ferramentaAtual);
        }

        Entities.Alocacao AlocacaoAtual 
        { get
            {
                if (ferramentaAtual is null)
                    return null;

                if (Alocacoes.Length == 0)
                    return null;

                var alocOptions = Entities.Ferramentas.GetAlocacaoDeFerramentaAlocada(Alocacoes, ferramentaAtual);
                if (FSharpOption<Entities.Alocacao>.get_IsSome(alocOptions))
                    return alocOptions.Value;
                else
                    return null;
            } 
        }
           
        protected override void OnInitialized()
        {
            Ferramentas = ferramentaRepository.GetAllFerramentas().ToArray();
            Alocacoes = Repositorio.GetAllAlocacoes().ToArray();
        }

        void OnPatrimonioChage(ChangeEventArgs args)
        {
            if (string.IsNullOrWhiteSpace((string)args.Value))
                return;

            ferramentaAtual = Ferramentas.Where(f => f.Patrimonio == Convert.ToInt32(args.Value)).SingleOrDefault();
            
        }

    }
}

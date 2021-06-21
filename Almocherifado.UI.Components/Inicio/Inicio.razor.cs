using AlmocharifadoApplication;
using Entities;
using Microsoft.AspNetCore.Components;
using System.Linq;


namespace Almocherifado.UI.Components.Inicio
{
    public  partial class Inicio
    {
        [Inject] public IAlmocharifadoRepository AlmocharifadoRepository { get; set; }
        [Inject] public IFerramentaRepository FerramentaRepository { get; set; }
        //CRIAR ABSTRAÇÃO PARA COMPONENTES QUE DEPENDEM DE FERRAMENTAS
        Ferramenta[] Ferramentas { get; set; }
        Entities.Alocacao[] Alocacoes { get; set; }

        int FerramentasCadastradas { get; set; }
        int FerramentasDisponiveis { get; set; }
        int FerramentasAlocadas { get; set; }


        Ferramenta ferramentaAtual { get; set; } = new Ferramenta();
        Entities.Alocacao[] AlocacoesAtuais { get; set; } = new Entities.Alocacao[] { };
        

        //TODO: CRIAR ABSTRAÇÃO PARA MEÉTODOS REPETIDOS, INVERSÃO DE DEPENDENCIA COM COMPOSIÇÃO
        void OnPatrimonioChage(ChangeEventArgs args)
        {
            if (string.IsNullOrWhiteSpace((string)args.Value))
                return;

            ferramentaAtual = Ferramentas.Where(f => f.Patrimonio == (string)args.Value).SingleOrDefault();
            AlocacoesAtuais = Alocacoes.Where(a => a.Ferramentas.Any(f => f == ferramentaAtual)).ToArray();


        }


        protected override void OnInitialized()
        {
            Ferramentas = FerramentaRepository.GetAllFerramentas();
            Alocacoes = AlmocharifadoRepository.GetAllAlocacoes();

            FerramentasCadastradas = Ferramentas.Length;
            FerramentasDisponiveis = Ferramentas.Where(f => Entities.Ferramentas.FerramentaDisponivel(Alocacoes, f)).Count();
            FerramentasAlocadas = FerramentasCadastradas - FerramentasDisponiveis;



        }
    }
}

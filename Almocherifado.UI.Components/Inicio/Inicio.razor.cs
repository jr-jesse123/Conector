using Almocharifado.InfraEstrutura;
using AlmocharifadoApplication;
using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;


namespace Almocherifado.UI.Components.Inicio
{
    public static class StringExtensions
    {
        public static bool Contem(this string textoOriginal, string outroTexto)
            => textoOriginal.ToUpper().Contains(outroTexto.ToUpper());
    }

    public partial class Inicio
    {
        

        [Inject] public IAlmocharifadoRepository AlmocharifadoRepository { get; set; }
        [Inject] public IFerramentaRepository FerramentaRepository { get; set; }
        //CRIAR ABSTRAÇÃO PARA COMPONENTES QUE DEPENDEM DE FERRAMENTAS
        Ferramenta[] Ferramentas { get; set; }
        Entities.Alocacao[] Alocacoes { get; set; }

        int FerramentasCadastradas { get; set; }
        int FerramentasDisponiveis { get; set; }
        int FerramentasAlocadas { get; set; }

        string TextoPesquisado { get; set; }

        Ferramenta[] ferramentasAtuais { get; set; } = new Ferramenta[] { };
        Entities.Alocacao[] AlocacoesAtuais { get; set; } = new Entities.Alocacao[] { };
        
        private int patrimonioAtual;
        int PatrimonioAtual 
            { 
                get => patrimonioAtual; 
                set { 
                        FerramentaSelecionada = Ferramentas.Where(f => f.Patrimonio == value).SingleOrDefault(); 
                        patrimonioAtual = value;
                        StateHasChanged();
                    } 
            }
        Ferramenta FerramentaSelecionada { get; set; }

        //TODO: CRIAR ABSTRAÇÃO PARA MEÉTODOS REPETIDOS, INVERSÃO DE DEPENDENCIA COM COMPOSIÇÃO
        void OnPatrimonioChage(ChangeEventArgs args)
        {


            if (string.IsNullOrWhiteSpace((string)args.Value))
            {
                PatrimonioAtual = 0;
                return;
            }
            PatrimonioAtual = Convert.ToInt32(args.Value);

            ferramentasAtuais = Ferramentas.Where(f => f.Patrimonio == Convert.ToInt32(args.Value)).ToArray();
            AlocacoesAtuais = Alocacoes
                    .Where(a => a.FerramentasAlocadas
                                .Any(f => ferramentasAtuais.Contains(f.Ferramenta)))
                                .ToArray();


        }

        protected override void OnInitialized()
        {
            Ferramentas = FerramentaRepository.GetAllFerramentas().ToArray();
            Alocacoes = AlmocharifadoRepository.GetAllAlocacoes().ToArray();

            FerramentasCadastradas = Ferramentas.Length;
            FerramentasDisponiveis = Ferramentas.Where(f => Entities.Ferramentas.FerramentaDisponivel(Alocacoes, f)).Count();
            FerramentasAlocadas = FerramentasCadastradas - FerramentasDisponiveis;



        }
    }
}

using AlmocharifadoApplication;
using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Ferramentas
{

    public partial class ListarFerramentas
    {

        void ToogleManutencao(Ferramenta ferramenta,bool valor)
        {
            if (valor)
                Ferramentasrepo.EnviarFerramentaParaManutencao(ferramenta);
            else
                Ferramentasrepo.FinalizarManutencao(ferramenta);
        }


        bool enabledAtribute { get => !(FerramentasChecadas.Count > 0) ; }

        Modal modalBaixa;
        void baixar()
        {
            foreach (var item in FerramentasChecadas)
            {
                Ferramentasrepo.BaixarFerramenta(item);
            }

            FerramentasChecadas.Clear();
            modalBaixa.Hide();
        }

        //IEnumerable<string> textoConfirmacaoBaixa { get; set; }

        //IEnumerable<string> getTextoConfirmacaoBaixa()
        //{
        //    return FerramentasChecadas.Select(f => $"{f.Nome } Patrimônio: {f.Patrimonio}");
        //}


        void FiltrarPorTextoLivre(string valor)
        {   
            //var valor = ((string)args.Value).ToUpper();
            if (string.IsNullOrWhiteSpace(valor))
                FerramentasVisiveis = Ferramentas;
            
            FerramentasVisiveis =
                    Ferramentas
                    .Where(f => ( f.Descricao is not null && f.Descricao.ToUpper().Contains(valor.ToUpper()))
                              | f.Marca.ToUpper().Contains(valor.ToUpper())
                              | f.Modelo.ToUpper().Contains(valor.ToUpper())
                              | f.Nome.ToUpper().Contains(valor.ToUpper())
                         ).ToArray();
        }

        void FiltrarPorStatus(ChangeEventArgs args)
        {
            var valor = (string)args.Value;
            switch (valor)
            {
                case "Disponível":
                    FerramentasVisiveis = Ferramentas.Where(f => Entities.Ferramentas.FerramentaDisponivel(Alocacoes, f)).ToArray();
                    break;
                case "Em Manutenção":
                    FerramentasVisiveis = Ferramentas.Where(f => f.EmManutencao).ToArray();
                    break;
                case "Alocada":
                    FerramentasVisiveis = Ferramentas.Where(f => !Entities.Ferramentas.FerramentaDisponivel(Alocacoes, f)).ToArray();
                    break;
                case "Baixada:":
                    FerramentasVisiveis = Ferramentas.Where(f => f.Baixada).ToArray();
                    break;

                default:
                    FerramentasVisiveis = Ferramentas;
                    break;
            }
        }
        [Inject] IFerramentaRepository Ferramentasrepo { get; set; }
        [Inject] IAlmocharifadoRepository repo { get; set; }
        [Inject] NavigationManager navMan { get; set; }
        Ferramenta[] Ferramentas { get; set; }
        Ferramenta[] FerramentasVisiveis { get; set; }
        List<Ferramenta> FerramentasChecadas { get; set; } = new();
        Entities.Alocacao[] Alocacoes { get; set; }
        protected override void OnInitialized()
        {
            Ferramentas = Ferramentasrepo.GetAllFerramentas();
            FerramentasVisiveis = Ferramentas;
            Alocacoes = repo.GetAllAlocacoes();
        }

        void AdicionarOuRemoverFerramentaChecada(Ferramenta ferramenta)
        {
            if (FerramentasChecadas.Contains(ferramenta))
                FerramentasChecadas.Remove(ferramenta);
            else
                FerramentasChecadas.Add(ferramenta);
        }

    }
}

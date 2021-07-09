using Almocharifado.InfraEstrutura;
using AlmocharifadoApplication;
using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Funcionarios
{
    public partial class ListarFuncionarios
    {   [Inject] NavigationManager NavMan { get; set; }
        [Inject] IAlmocharifadoRepository AlmocharifadoRepository { get; set; }
        [Inject] IFerramentaRepository FerramentaRepository { get; set; }

        
        Dictionary<Funcionario, FerramentaAlocadaInfo[]> funcionariosEFerramentas { get; set; }
        //ATENÇÃO: funcionarios e alocações não possui todos os funciona´rios
        Dictionary<Funcionario, Entities.Alocacao[]> funcionariosEAlocacoes { get; set; }
        Dictionary<Funcionario, FerramentaAlocadaInfo[]> funcionariosEFerramentasSelecionados { get; set; }
        //ATENÇÃO: funcionarios e alocações não possui todos os funciona´rios
        

        protected override void OnInitialized()
        {
            var funcionarios = AlmocharifadoRepository.GetAllFuncionarios();
            var alocacoes = AlmocharifadoRepository.GetAllAlocacoes();
            var ferrametnas = FerramentaRepository.GetAllFerramentas();

            var ferramentasPorFunc = alocacoes
                    .Select(aloc => (aloc.Responsavel,aloc.FerramentasAlocadas))
                    .GroupBy(tuple => tuple.Responsavel)
                    .Select(g =>
                        new KeyValuePair<Funcionario,FerramentaAlocadaInfo[]>(g.Key, g.SelectMany(g => g.FerramentasAlocadas).ToArray()
                    )
                );

            var funcComFerramentasDic = new Dictionary<Funcionario, FerramentaAlocadaInfo[]>(ferramentasPorFunc);

            foreach (var funcionario in funcionarios)
            {
                if (!funcComFerramentasDic.Keys.Contains(funcionario))
                    funcComFerramentasDic.Add(funcionario, new FerramentaAlocadaInfo[] { });
            }

            funcionariosEFerramentas = funcComFerramentasDic;

            funcionariosEFerramentasSelecionados = funcionariosEFerramentas;

            var alocacoesPorFuncionario = alocacoes
                .Select(aloc => (aloc.Responsavel, aloc))
                .GroupBy(aloc => aloc.Responsavel)
                //.SelectMany(aloc => aloc)
                .Select(aloc => new KeyValuePair<Funcionario, Entities.Alocacao[]>(
                    aloc.Key, aloc.Select(g => g.aloc).ToArray()
                    ));

            funcionariosEAlocacoes = new Dictionary<Funcionario, Entities.Alocacao[]>(alocacoesPorFuncionario);

            foreach (var funcionario in funcionarios)
            {
                if (!funcionariosEAlocacoes.Keys.Contains(funcionario))
                    funcionariosEAlocacoes.Add(funcionario, new Entities.Alocacao[] { });
            }

        }

        void OnPesquisaPorNomeFuncionario(ChangeEventArgs args)
        {
            if (string.IsNullOrWhiteSpace((string)args.Value))
            {
                funcionariosEFerramentasSelecionados = funcionariosEFerramentas;
                return;
            }

            var funcionariosFiltradods = funcionariosEFerramentas.Where(f => f.Key.Nome.ToUpper().Contains(args.Value.ToString().ToUpper()));

            funcionariosEFerramentasSelecionados = new (funcionariosFiltradods);               ;

        }
    }
}

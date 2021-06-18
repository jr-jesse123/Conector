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

        
        Dictionary<Funcionario,Ferramenta[]> funcionariosEFerramentas { get; set; }
        //ATENÇÃO: funcionarios e alocações não possui todos os funciona´rios
        Dictionary<Funcionario, Entities.Alocacao[]> funcionariosEAlocacoes { get; set; }
        Entities.Alocacao[] alocacoes { get; set; }

        protected override void OnInitialized()
        {
            var funcionarios = AlmocharifadoRepository.GetAllFuncionarios();
            var alocacoes = AlmocharifadoRepository.GetAllAlocacoes();
            var ferrametnas = FerramentaRepository.GetAllFerramentas();

            var ferramentasPorFunc = alocacoes
                    .Select(aloc => (aloc.Responsavel,aloc.Ferramentas))
                    .GroupBy(tuple => tuple.Responsavel)
                    .Select(g =>
                        new KeyValuePair<Funcionario,Ferramenta[]>(g.Key, g.SelectMany(g => g.Ferramentas).ToArray()
                    )
                );

            var funcComFerramentasDic = new Dictionary<Funcionario,Ferramenta[]>(ferramentasPorFunc);

            foreach (var funcionario in funcionarios)
            {
                if (!funcComFerramentasDic.Keys.Contains(funcionario))
                    funcComFerramentasDic.Add(funcionario, new Ferramenta[] { });
            }

            funcionariosEFerramentas = funcComFerramentasDic;


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
    }
}

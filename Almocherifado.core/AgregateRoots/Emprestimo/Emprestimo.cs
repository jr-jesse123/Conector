using Almocherifado.core.AgregateRoots.FerramentaNm;
using System;
using System.Collections.Generic;
using System.Linq;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;

namespace Almocherifado.core.AgregateRoots.EmprestimoNm
{
    public class Emprestimo
    {
        protected Emprestimo() { }
        public Emprestimo(DateTime entrega, Funcionario  funcionario, string obra, params Ferramenta[] ferramentas)
        {
            Entrega = entrega;
            Funcionario = funcionario;
            _ferramentasEmprestas = ferramentas.Select(ferramenta => new FerramentaEmprestada(ferramenta)).ToList();
            Obra = obra;
        }

        public int Id { get; set; }
        public DateTime Entrega { get; }
        public virtual Funcionario Funcionario { get; }
        private readonly List<FerramentaEmprestada> _ferramentasEmprestas = new List<FerramentaEmprestada>();
        public virtual List<FerramentaEmprestada> FerramentasEmprestas { get => _ferramentasEmprestas.ToList(); }
        public string Obra { get; }
        public string TermoResponsabilidade { get; }
        public bool Finalizado { get => FerramentasEmprestas.Any(f => f.DataDevolucao is null) ? false : true; }


    }
}

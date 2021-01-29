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
            if (string.IsNullOrEmpty(obra))
            {
                throw new ArgumentException($"'{nameof(obra)}' não pode ser nulo ou vazio", nameof(obra));
            }

            if (ferramentas is null)
            {
                throw new ArgumentNullException(nameof(ferramentas));
            }

            Entrega = entrega;
            Funcionario = funcionario ?? throw new ArgumentNullException(nameof(funcionario));
            _ferramentasEmprestas = ferramentas.Select(ferramenta => new FerramentaEmprestada(ferramenta)).ToList();
            Obra = obra;
        }

        public int Id { get; }
        public DateTime Entrega { get; }
        public virtual Funcionario Funcionario { get; }
        private readonly List<FerramentaEmprestada> _ferramentasEmprestas = new List<FerramentaEmprestada>();
        public virtual List<FerramentaEmprestada> FerramentasEmprestas { get => _ferramentasEmprestas.ToList(); }
        public string Obra { get; }
        public string TermoResponsabilidade { get; }
        public bool Finalizado { get => FerramentasEmprestas.Any(f => f.DataDevolucao is null) ? false : true; }


    }
}

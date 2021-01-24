using System;
using System.Collections.Generic;
using System.Linq;

namespace Almocherifado.core
{
    public class Emprestimo
    {
        protected Emprestimo() { }
        public Emprestimo(DateTime entrega, Funcionario funcionario, string obra, params Ferramenta[] ferramentas)
        {
            Entrega = entrega;
            Funcionario = funcionario;
            this.ferramentas = ferramentas.Select(ferramenta => new FerramentaEmprestada(ferramenta)).ToList();
            Obra = obra;
        }

        public int Id { get; set; }
        public DateTime Entrega { get; }
        public virtual Funcionario Funcionario { get; }
        protected virtual List<FerramentaEmprestada> ferramentas {get;set;}
        public virtual IReadOnlyList<FerramentaEmprestada> Ferramentas { get => ferramentas.ToList(); }
        public string Obra { get; set; }

        public bool Finalizado { get => Ferramentas.Any(f => f.DataDevolucao is null) ? false : true; }

    }

    public class FerramentaEmprestada
    {
        private FerramentaEmprestada()
        {

        }
        public FerramentaEmprestada(Ferramenta ferramenta)
        {
            Ferramenta = ferramenta;
        }

        public void AcusarRecebimento()
        {
            DataDevolucao = DateTime.Now;
        }

        public int Id { get; }

        public DateTime? DataDevolucao { get; private set; }
        public Ferramenta Ferramenta { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            this._ferramentas = ferramentas.Select(ferramenta => new FerramentaEmprestada(ferramenta)).ToList();
            Obra = obra;
        }

        public int Id { get; set; }
        public DateTime Entrega { get; }
        public virtual Funcionario Funcionario { get; }
        protected virtual ICollection<FerramentaEmprestada> _ferramentas {get;set;}



        public virtual IReadOnlyList<FerramentaEmprestada> Ferramentas { get => _ferramentas.ToList(); }
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
        public virtual Emprestimo Emprestimo { get; set; }
        public DateTime? DataDevolucao { get; private set; }
        public virtual Ferramenta Ferramenta { get; }
    }
}

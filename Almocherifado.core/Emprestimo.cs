using System;
using System.Collections.Generic;
using System.Linq;

namespace Almocherifado.core
{
    public class Emprestimo
    {
        private Emprestimo() { }
        public Emprestimo(DateTime entrega, Funcionario funcionario, string obra, params Ferramenta[] ferramentas)
        {
            Entrega = entrega;
            Funcionario = funcionario;
            _ferramentas = ferramentas.Select(ferramenta => new FerramentaEmprestada(ferramenta)).ToList();
            Obra = obra;
        }

        public int Id { get; set; }
        public DateTime Entrega { get; }
        public DateTime? Devolucao { get; }
        public Funcionario Funcionario { get; }
        private ICollection<FerramentaEmprestada> _ferramentas {get;set;}
        public IReadOnlyList<FerramentaEmprestada> Ferramentas { get => _ferramentas.ToList(); }
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

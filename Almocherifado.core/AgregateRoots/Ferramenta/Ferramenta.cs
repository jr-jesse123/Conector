using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.core.AgregateRoots.FerramentaNm
{
    [DebuggerDisplay("id:{Id},Nome:{NomeAbreviado}")]
    public class Ferramenta : ValueObject<Ferramenta>
    {
        protected Ferramenta() { }

        public Ferramenta(string nomeAbreviado, string descricao, DateTime dataCompra, string fotoUrl, string marca, string modelo)
        {
            NomeAbreviado = nomeAbreviado;
            Descricao = descricao;
            DataCompra = dataCompra;
            FotoUrl = fotoUrl;
            Marca = marca;
            Modelo = modelo;
        }

        public int Id { get; }
        public string NomeAbreviado { get; }
        public string Marca { get;  }
        public string Modelo { get; }
        public string Descricao { get; }
        public DateTime DataCompra { get; }
        public string FotoUrl { get; }
        public virtual IReadOnlyCollection<FerramentaEmprestada> HistoricoEmprestimos { get; }


        public bool Emprestada { get; }

        protected override bool EqualsCore(Ferramenta other)
        {
            return other.Id == Id;
        }

        protected override int GetHashCodeCore()
        {
            return Id.GetHashCode();
        }
    }
}

using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.core
{
    [DebuggerDisplay("id:{Id},Nome:{NomeAbreviado}")]
    public class Ferramenta : ValueObject<Ferramenta>
    {
        protected Ferramenta() { }

        public Ferramenta(string nomeAbreviado, string descrição, DateTime dataCompra, string fotoUrl)
        {
            NomeAbreviado = nomeAbreviado;
            Descrição = descrição;
            DataCompra = dataCompra;
            FotoUrl = fotoUrl;
        }



        public int Id { get;  }
        public string NomeAbreviado { get;  }
        public string Descrição { get;  }
        public DateTime DataCompra { get;  }
        public string FotoUrl { get;  }
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

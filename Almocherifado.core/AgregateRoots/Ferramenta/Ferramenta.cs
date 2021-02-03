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
    public class Ferramenta : Entity
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

        public int Id { get; private set; }
        public string NomeAbreviado { get; }
        public string Marca { get;  }
        public string Modelo { get; }
        public string Descricao { get; }
        public DateTime DataCompra { get; }
        public string FotoUrl { get; private set; }
        public virtual IReadOnlyCollection<FerramentaEmprestada> HistoricoEmprestimos { get; }


        public bool Emprestada 
        { 
            get  
            {
                if (HistoricoEmprestimos is not null)
                    return HistoricoEmprestimos.Any(e => !e.DataDevolucao.HasValue);
                else
                    return false;
            } 
        }




        public override string ToString()
        {
            return $"{NomeAbreviado} | {Marca} | {Modelo}";
        }

    }
}

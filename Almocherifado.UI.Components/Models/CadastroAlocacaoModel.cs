using System;
using System.Collections.Generic;
using Entities;

namespace Almocherifado.UI.Components.Models
{
    public record CadastroAlocacaoModel
    {
        
        public IEnumerable<Ferramenta> Ferramentas { get; set; }
        
        public Funcionario Responsavel { get; set; }
        
        public string ContratoLocacao { get; set; }
        
        public DateTime Data { get; set; }
    }

}

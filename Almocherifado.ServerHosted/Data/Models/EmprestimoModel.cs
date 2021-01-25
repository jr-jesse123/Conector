using Almocherifado.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Data.Models
{
    public class EmprestimoModel
    {
        public DateTime Entrega { get; set; }
        public FuncionarioModel Funcionario { get; set; }
        public List<Ferramenta> Ferramentas { get; set; }
        public string   Obra { get; set; }
    }

    //TODO: CONTINUAR CRIANDO A VALIDAÇÃO DE EMPRÉSTIMO´E O FORMULÁRIO PARA ADICIONAR NOVO EMPRESTIMO

}

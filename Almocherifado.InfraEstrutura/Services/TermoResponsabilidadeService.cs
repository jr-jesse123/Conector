using Almocherifado.core.AgregateRoots.EmprestimoNm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace Almocherifado.core.Services
{
    public class TermoResponsabilidadeService
    {
        public DocX GetTermoResponsabilidade(Emprestimo emprestimo)
        {
            using var documento = DocX.Load("documento.docx");

            documento.ReplaceText("#NomeCompleto", emprestimo.Funcionario.Nome);
            documento.ReplaceText("#cpf", emprestimo.Funcionario.CPF.ToString());

            var ferramentas = emprestimo.FerramentasEmprestas
                .Select(fe => fe.Ferramenta.ToString())
                .Aggregate((a, b) => a + Environment.NewLine + b);

            documento.ReplaceText("#Ferramentas", ferramentas);

            documento.ReplaceText("#dia", ferramentas);
            documento.ReplaceText("#Mes", ferramentas);
            documento.ReplaceText("#Ano", ferramentas);

            return documento;

        }


        
       
    }

    
}

using Almocherifado.core.AgregateRoots.EmprestimoNm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace Almocherifado.core.Services
{
    public class TermoResponsabilidadeService
    {
        DocX documento;
        public TermoResponsabilidadeService(ModeloTermoService modeloTermoService)
        {
            documento = modeloTermoService.GetModelo();
        }

        public DocX GetTermo(Emprestimo emprestimo)
        {
            documento.ReplaceText("#NomeCompleto", emprestimo.Funcionario.Nome);
            documento.ReplaceText("#cpf", emprestimo.Funcionario.CPF.ToString());
            documento.ReplaceText("#Obra", emprestimo.Obra);

            var ferramentas = emprestimo.FerramentasEmprestas
                .Select(fe => fe.Ferramenta.ToString())
                .Aggregate((a, b) => a + Environment.NewLine + b);

            documento.ReplaceText("#Ferramentas", ferramentas);

            documento.ReplaceText("#dia", emprestimo.Entrega.Day.ToString("00"));
            documento.ReplaceText("#Mes", emprestimo.Entrega.Month.ToString("00"));
            documento.ReplaceText("#Ano", emprestimo.Entrega.Year.ToString("0000"));

            return documento;
        }

    }


    public class ModeloTermoService
    {
        readonly string basepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public DocX GetModelo()
        {
            lock (this)
            {
                using var documento = DocX.Load(basepath + @"\Services\Modelo de Responsabilidade de equipamentos.docx");
                return documento.Copy();
            }
        }
    }

}

using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
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

        public DocX GetTermoPreenchido(DateTime DataEntrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra)
        {
            documento.ReplaceText("#NomeCompleto", funcionario.Nome);
            documento.ReplaceText("#cpf", funcionario.CPF.ToString());
            documento.ReplaceText("#Obra", Obra);

            var ferramentastext = ferramentas
                .Select(f => f.ToString())
                .Aggregate((a, b) => a + Environment.NewLine + b);

            documento.ReplaceText("#Ferramentas", ferramentastext);

            documento.ReplaceText("#dia", DataEntrega.Day.ToString("00"));
            documento.ReplaceText("#Mes", DataEntrega.Month.ToString("00"));
            documento.ReplaceText("#Ano", DataEntrega.Year.ToString("0000"));

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

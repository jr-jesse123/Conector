using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.InfraEstrutura.Tests
{
    public class WordToPDfTests
    {
        [Fact]
        public void Arquivo_Word_Eh_Convertido_Para_PDF()
        {
            var covnerter = new WordToPDFService();

            var basepath = Directory.GetCurrentDirectory(); 
                           //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            covnerter.ExportarWordParaPdf(basepath + @"\Services\Modelo de Responsabilidade de equipamentos.docx");
        } 
    }
}

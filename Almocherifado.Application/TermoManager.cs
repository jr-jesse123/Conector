using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura;
using Almocherifado.ServerHosted.Helpers.FileHelpers;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.ServerHosted.Services
{
    public class TermoManager : ITermoManager
    {
        private readonly ITermoResponsabilidadeService termoService;
        private readonly IWordToPDFService pdfconversor;
        private readonly string basepath;

        public TermoManager(ITermoResponsabilidadeService termoService,
            IWordToPDFService pdfconversor, IPathHelper pathHelper)
        {
            this.termoService = termoService;
            this.pdfconversor = pdfconversor;
            

            basepath = pathHelper.FotosTermos_Location;
        }

        public async Task<Result<string>> BuildTermo(DateTime DataEntrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra)
        {
            //try
            {
                var output = await Task.Run(() =>
                {
                    var documento = termoService.GetTermoPreenchido(DataEntrega, funcionario, ferramentas, Obra);
                    Assert.NotEmpty(documento.Text);
                    var output = basepath + @"\" + DataEntrega.ToFileTime() + ".docx";
                    Directory.CreateDirectory(Path.GetDirectoryName(output));
                    documento.SaveAs(output);
                    pdfconversor.ExportarWordParaPdf(output);
                    return output.Replace(".docx", ".pdf");
                });

                return output;
            }
            //catch (Exception ex)
            //{
            //    return Result.Failure<string>(ex.Message);
            //}

        }
    }
}

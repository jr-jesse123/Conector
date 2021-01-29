﻿using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura;
using Almocherifado.ServerHosted.Helpers.FileHelpers;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Almocherifado.ServerHosted.Services
{
    public class TermoManager
    {
        private readonly TermoResponsabilidadeService termoService;
        private readonly WordToPDFService pdfconversor;
        private readonly IPathHelper pathHelper;
        private readonly string basepath;

        public TermoManager(TermoResponsabilidadeService termoService,
            WordToPDFService pdfconversor, IPathHelper pathHelper)
        {
            this.termoService = termoService;
            this.pdfconversor = pdfconversor;
            this.pathHelper = pathHelper;

            basepath = pathHelper.FotosTermos_Location;
        }

        public Result<string> BuildTermo(DateTime DataEntrega, Funcionario funcionario, List<Ferramenta> ferramentas, string Obra)
        {
            try
            {
                var documento = termoService.GetTermoPreenchido(DataEntrega, funcionario, ferramentas, Obra);
                var output = basepath + @"\" + DataEntrega.ToFileTime() + ".docx";

                documento.SaveAs(output);

                pdfconversor.ExportarWordParaPdf(output);

                output = output.Replace(".docx", ".pdf");

                return output;
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
             
        }
    }
}
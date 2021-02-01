using System;
using System.Collections.Generic;
using System.IO;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using Almocherifado.core.Tests;
using Almocherifado.InfraEstrutura;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Almocherifado.Application.Tests
{
    public class TermoManagerTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TermoManagerTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Theory, DomainAutoData]
        void Termo_Solicitado_Eh_Produzio_Em_Pdf(Mock<IPathHelper> mockPathHelper,   WordToPDFService pdfService
            , Funcionario funcionario, List<Ferramenta> ferramentas, string obra, DateTime dataentrega)
        {
            TermoResponsabilidadeService termoService = new(new ModeloTermoService());
            //var termoservice = new TermoResponsabilidadeService(new ModeloTermoService());

            mockPathHelper.Setup(ph => ph.FotosTermos_Location).Returns(Directory.GetCurrentDirectory());

            var sut  = new TermoManager(termoService,pdfService, mockPathHelper.Object);

            var result =  sut.BuildTermo(dataentrega, funcionario, ferramentas, obra);

            testOutputHelper.WriteLine($"Saida:");
            testOutputHelper.WriteLine(mockPathHelper.Object.FotosTermos_Location);
        }
    }
}

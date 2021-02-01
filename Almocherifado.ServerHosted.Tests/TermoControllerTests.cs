using Almocherifado.Application;
using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura;
using Almocherifado.UI.Controllers;
using Almocherifado.UI.Helpers.FileHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace Almocherifado.UI.Tests
{
    public class TermoControllerTests
    {
        public TermoControllerTests()
        {

        }
        [Theory, UiAutoData]
        public void Termo_Controle_Recebe_Parametros_E_Identifica_O_Arquivo_Corretamente(Mock<IWebHostEnvironment> HostMock,
             WordToPDFService toPDFService,
            Funcionario funcionario, List<Ferramenta> ferramentas, string obra, DateTime dataentrega)
        {

            var responsabilidadeService = new TermoResponsabilidadeService(new ModeloTermoService());

            HostMock.Setup(h => h.WebRootPath).Returns(Directory.GetCurrentDirectory());

            string Resultfile = new TermoManager(responsabilidadeService, toPDFService,
                new PathHelper(HostMock.Object))
                .BuildTermo(dataentrega, funcionario, ferramentas, obra).Result.Value;

            var controller = new TermoController(HostMock.Object);

            IActionResult result = controller.Get(Resultfile);

            var arquivo = result as FileStreamResult;

            arquivo.Should().NotBeNull();

        }
    }
}

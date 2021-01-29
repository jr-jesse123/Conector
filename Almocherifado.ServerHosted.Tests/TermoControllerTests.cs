using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura;
using Almocherifado.ServerHosted.Controllers;
using Almocherifado.ServerHosted.Helpers.FileHelpers;
using Almocherifado.ServerHosted.Services;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Almocherifado.ServerHosted.Tests.Mappingtests;

namespace Almocherifado.ServerHosted.Tests
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
                .BuildTermo(dataentrega, funcionario, ferramentas, obra).Value;

            var controller = new TermoController(HostMock.Object);

            IActionResult result = controller.Get(Resultfile);

            var arquivo = result as FileStreamResult;

            arquivo.Should().NotBeNull();

        }
    }
}

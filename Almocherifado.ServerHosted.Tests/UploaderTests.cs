using Almocherifado.UI.Components.Helpers;
using Almocherifado.UI.Components.Models;
using AutoFixture;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Almocherifado.UI.Tests
{
    public class UploaderTests
    {
        private readonly ITestOutputHelper outputHelper;

        public UploaderTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }
        [Fact]
        public void teste1()
        {
            var ferramenta = new Fixture()
                .Build<CadastroFerramentaModel>()
                .Without(f => f.Fotos)
                .With(f => f.Nome, "FerramentaTeste")
                .Create();

            outputHelper.WriteLine(ferramenta.Nome);

            var bytes = File.ReadAllBytes("wwwroot/background.png");

            var ms = new MemoryStream(bytes);

            FileHelper.SaveFileToRoot(ms, "teste.png");
            
            //FileHelper.getFotoFerramentaPath(ferramenta);

        }
    }
}

using Bunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.UI.Tests
{

    public class WebRooTests : TestContext
    {
        private readonly HttpClient client;
        private readonly WebApplicationFactory<Startup> _factory;

        public WebRooTests()
        {
            client = new WebApplicationFactory<Startup>().CreateClient();
        }



        [Fact]
        public async Task TestePathAsync()
        {
            var result = await client.GetAsync("/fotosFerramentas/teste.txt");
            result.EnsureSuccessStatusCode();
        }

    }
}

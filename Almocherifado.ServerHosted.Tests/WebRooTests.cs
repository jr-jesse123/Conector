using Almocherifado.ServerHosted.Helpers.FileHelpers;
using Bunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.ServerHosted.Tests
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

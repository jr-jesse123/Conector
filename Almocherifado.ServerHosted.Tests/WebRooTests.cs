using Almocherifado.InfraEstrutura;
using Bunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Almocherifado.UI.Tests
{

    public class WebRooTests : TestContext
    {
        private readonly HttpClient client;
        

        public WebRooTests()
        {
            client = new WebApplicationFactory<Startup>()
                
                .CreateDefaultClient();
        }



        [Fact]
        public async Task TestePathAsync()
        {
            var result = await client.GetAsync("/fotosFerramentas/teste.txt");
            result.EnsureSuccessStatusCode();
        }

    }


    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDbContext<AlmocherifadoContext>(x =>
                x.UseSqlite(@"Data Source = almocherifadotests.db")
                .UseLazyLoadingProxies());

        }
    }

}

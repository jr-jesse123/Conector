using Almocherifado.core.Tests;
using Almocherifado.ServerHosted.Shared.FormularioCadastroImprestimo;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.JSInterop;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Xunit;
using static Bunit.ComponentParameterFactory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Almocherifado.ServerHosted.Tests.NovaPasta
{
    public class TestesBinding : TestContext
    {

        public TestesBinding()
        {
            Services
                .AddBlazorise()
                .AddBootstrapProviders();
             

        }

        [Theory, DomainAutoData]
        public void Teste_Bindin_Data_De_Entrega(DateTime DataToInput)
        {

            JSInterop.Mode = JSRuntimeMode.Strict;

            DateTime DataControl = default(DateTime);

            var dataParam = Parameter(nameof(DataDeEntrega.Data), DataControl);
            
            var callbackParam =
                EventCallback<DateTime>(nameof(DataDeEntrega.DataChanged), (nvData) => DataControl = nvData);


            var sut = RenderComponent<DataDeEntrega>(dataParam, callbackParam);

            sut.Find("input").Change(new() {  Value= DataToInput});

            DataControl.Should().NotBe(default(DateTime));
            DataControl.Should().Be(DataToInput);
        }
    }
}

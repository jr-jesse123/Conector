using Almocherifado.core.Tests;
using Almocherifado.ServerHosted.Shared.FormularioCadastroImprestimo;
using Blazorise;
using Blazorise.Bootstrap;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using System;
using Xunit;
using static Bunit.ComponentParameterFactory;

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
            DateTime DataControl = default(DateTime);

            var dataParam = Parameter(nameof(DataDeEntrega.Data), DataControl);
            
            var callbackParam =
                EventCallback<DateTime>(nameof(DataDeEntrega.DataChanged), (nvData) => DataControl = nvData);

            var sut = RenderComponent<DataDeEntrega>(dataParam, callbackParam);

            sut.Find("input").Change(new() {  Value= DataToInput});

            DataControl.Should().NotBe(default(DateTime));
            DataControl.Should().Be(DataToInput);
        }


        [Theory, DomainAutoData]
        public void Teste_Bindin_Obra_Input(string ObraStrInput)
        {
            string ObraStrControle = "";

            JSInterop.Mode = JSRuntimeMode.Loose;

            var ObraControleParam = Parameter(nameof(ObraInput.Obra), ObraStrControle);
            var callbackParam = EventCallback<string>(nameof(ObraInput.ObraChanged), (nvObraStr) => ObraStrControle = nvObraStr);

            var sut = RenderComponent<ObraInput>(ObraControleParam, callbackParam);

            sut.Find("input").Change(new ChangeEventArgs() { Value = ObraStrInput });

            ObraStrControle.Should().Be(ObraStrInput);

            //var sut = RenderComponent<DataDeEntrega>(dataParam, callbackParam);

            //sut.Find("input").Change(new() { Value = DataToInput });

            //DataControl.Should().NotBe(default(DateTime));
            //DataControl.Should().Be(DataToInput);
        }
    }
}

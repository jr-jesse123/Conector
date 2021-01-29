using Almocherifado.core.AgregateRoots.FerramentaNm;
using Almocherifado.InfraEstrutura.Repositorios;
using Almocherifado.ServerHosted.Helpers.FileHelpers;
using Almocherifado.ServerHosted.Shared.Components;
using AutoFixture.Xunit2;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Almocherifado.ServerHosted.Tests.Mappingtests;
using static Bunit.ComponentParameterFactory;

using AutoFixture;
using Blazorise;
using Blazorise.Bootstrap;
using Almocherifado.InfraEstrutura;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace Almocherifado.ServerHosted.Tests
{
    public class FerramentasGridTests : TestContext
    {
        public FerramentasGridTests()
        {
            Services.AddBlazorise()
                .AddBootstrapProviders();

            JSInterop.Mode = JSRuntimeMode.Loose;
        }

        [Theory, UiAutoData]
        public void Ferramentas_Adicionadas_E_Removidas_Do_FerramentaGrid_Sao_Refletidas_No_Parente(UIFixture fixtres)
        {
            List<Ferramenta> ferramentas = fixtres.CreateMany<Ferramenta>(10).ToList();

            var options = new DbContextOptionsBuilder<AlmocherifadoContext>().UseInMemoryDatabase("Teste").Options;
            var context = new AlmocherifadoContext(options);
            context.AddRange(ferramentas);
            context.SaveChanges();
            ferramentas = context.Ferramentas.ToList();
            
            var repositoryMock = new Mock<IFerramentaRepository>();
            var pathHelperMock = new Mock<IPathHelper>( MockBehavior.Strict);
            pathHelperMock.SetupAllProperties();

            repositoryMock.Setup(r => r.GetallFerramentas()).Returns(ferramentas);

            Services.Add(new ServiceDescriptor(typeof(IFerramentaRepository), repositoryMock.Object));
            Services.Add(new ServiceDescriptor(typeof(IPathHelper), pathHelperMock.Object));

            var ferramentaListControl = new List<Ferramenta>();

            var FerramentasListSourceParam = Parameter(nameof(FerramentasGrid.FerramentasChecadas), ferramentaListControl);

            var sut = RenderComponent<FerramentasGrid>(FerramentasListSourceParam);

            //addiona a primeira ferramenta
            sut.FindComponents<Blazorise.Switch<bool>>().First()
                .Find("input").Change(new ChangeEventArgs() { Value = true});

            ferramentaListControl.Count.Should().Be(1);

            //adiciona a terceira ferramenta
            sut.FindComponents<Blazorise.Switch<bool>>()[3]
                .Find("input").Change(new ChangeEventArgs() { Value = true });

            ferramentaListControl.Count.Should().Be(2);

            //remove a primeira ferramenta
            sut.FindComponents<Blazorise.Switch<bool>>()[0]
                .Find("input").Change(new ChangeEventArgs() { Value = false });

            ferramentaListControl.Count.Should().Be(1);

        }
    }
}

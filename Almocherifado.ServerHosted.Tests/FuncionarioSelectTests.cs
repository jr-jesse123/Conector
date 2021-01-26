using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Tests;
using Almocherifado.ServerHosted.Shared;
using Almocherifado.ServerHosted.Shared.FormularioCadastroImprestimo;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Xunit;


using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Almocherifado.ServerHosted.Tests
{
    public class FuncionarioSelectTests : TestContext , IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;

        public FuncionarioSelectTests(ITestOutputHelper testOutputHelper)
        {
            Services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true; // optional
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
            this.testOutputHelper = testOutputHelper;
        }


        [Theory, DomainAutoData]
        public void Mudancas_Do_Cpf_Sao_Refletidas_No_CallBakc(List<Funcionario> funcionarios)
        {
            string MyProperty = "";

             var ParentTitle = ComponentParameterFactory.Parameter(nameof(FuncionarioSelect.ParentsCPF), MyProperty);

            var callback = ComponentParameterFactory.EventCallback<string>(nameof(FuncionarioSelect.ParentsCPFChanged),
                args => MyProperty = args.ToString());

            var cut = RenderComponent<FuncionarioSelect>(ParentTitle, callback);

            cut.Find("select").Change(new ChangeEventArgs() { Value = funcionarios[2].CPF });

            MyProperty.Should().Be(funcionarios[2].CPF.ToString());

        }

    }
    
}

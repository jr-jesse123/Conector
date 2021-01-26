using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Tests;
using Almocherifado.ServerHosted.Shared.FormularioCadastroImprestimo;
using AngleSharp.Dom;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Bunit.ComponentParameterFactory;


namespace Almocherifado.ServerHosted.Tests
{
    public class FuncionarioSelectTests : TestContext
    {
        public FuncionarioSelectTests()
        {
            Services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true; // optional
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

        }

        [Theory, DomainAutoData]
        public void Mudancas_Do_Cpf_Sao_Refletidas_No_CallBakc(List<Funcionario> funcionarios)
        {
            string cpf = "";
            Action<ChangeEventArgs> cb = (args) => cpf = args.Value.ToString();

            var OnCpfescolhido = ComponentParameterFactory.EventCallback("OnCpfEscolhido", cb);
            var funcionariosParameter = ComponentParameterFactory.Parameter("FuncionariosSource", funcionarios);

            var cut = RenderComponent<FuncionarioSelect>(OnCpfescolhido,funcionariosParameter);


            cut.FindComponent<Select<string>>().Find("Select")
                .Change(new ChangeEventArgs() 
                {
                    Value = funcionarios[2].CPF
                });


            cpf.Should().Be(funcionarios[2].CPF.ToString());


        }
    }
}

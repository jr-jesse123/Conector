using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Tests;
using Almocherifado.UI.Shared.FormularioCadastroImprestimo;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Almocherifado.UI.Data.Models;
using System.Linq;

namespace Almocherifado.UI.Tests
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

            FuncionarioModel model = new();
            var ParentTitle = ComponentParameterFactory.Parameter(nameof(FuncionarioSelect.ParentsCPF), model);

            var callback = ComponentParameterFactory.EventCallback<FuncionarioModel>(nameof(FuncionarioSelect.ParentsCPFChanged),
                args => model = (args));

            var listaParam = ComponentParameterFactory.Parameter(nameof(FuncionarioSelect.FuncionariosSource), 
                funcionarios.Select(f => new FuncionarioModel { CPF = f.CPF, Email = f.Email, Nome = f.Nome }).ToList());

            var cut = RenderComponent<FuncionarioSelect>(ParentTitle, callback,listaParam);

            var func = funcionarios[2];

            var value = new FuncionarioModel {  CPF= func.CPF, Email = func.Email, Nome = func.Nome};

            cut.Find("select").Change(new ChangeEventArgs() { Value = value });

            CpfLibrary.Cpf.Format(model.CPF).Should().Be(funcionarios[2].CPF.ToString());
        }

    }
    
}

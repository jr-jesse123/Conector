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

        public string MyProperty { get; set; } = "";

        [Theory, DomainAutoData]
        public void Mudancas_Do_Cpf_Sao_Refletidas_No_CallBakc(List<Funcionario> funcionarios)
        {
            //Mock<Action<string>> mock = new Mock<Action<string>>();
            var Mock = "";

            EventCallback<string> callback
                = new EventCallback<string>();


            var OnCpfescolhido = ComponentParameterFactory.Parameter("ParentsCPF", MyProperty);
            var Onchanged = ComponentParameterFactory.Parameter("ParentsCPFChanged", callback);
            var funcionariosParameter = ComponentParameterFactory.Parameter("FuncionariosSource", funcionarios);

            var cut = RenderComponent<FuncionarioSelect>(OnCpfescolhido, Onchanged, funcionariosParameter);

            cut.FindComponent<Select<string>>().Find("Select")
                .Change(new ChangeEventArgs() 
                {
                    Value = funcionarios[2].CPF
                });


            //cut.FindComponent<Select<string>>().Instance.SelectedValue.Should().Be(funcionarios[2].CPF);

            MyProperty.Should().Be(funcionarios[2].CPF);

            //mock.Verify(a => a.Invoke(It.IsAny<ChangeEventArgs>()), Times.Once);
        }

        [Theory, DomainAutoData]
        void testeBind(List<Funcionario> funcionarios)
        {
            var ParentTitle = ComponentParameterFactory.Parameter(nameof(FuncionarioSelect.ParentsCPF), MyProperty);

            var callback = ComponentParameterFactory.EventCallback<string>(nameof(FuncionarioSelect.ParentsCPFChanged),
                args => MyProperty = args.ToString());

            var cut = RenderComponent<FuncionarioSelect>(ParentTitle, callback);

            cut.Find("select").Change(new ChangeEventArgs() { Value = funcionarios[2].CPF });

            //var cut = RenderComponent<FuncionarioSelect>(
            //builder =>
            //{
            //    builder.Add(c => c.ParentsCPF, MyProperty);
            //    builder.Add(c => c.ParentsCPFChanged, (e) =>
            //    {
            //        textChangedEventTriggered = e;
            //    }) );
            //});




            MyProperty.Should().Be(funcionarios[2].CPF.ToString());

        }

        void teste2()
        {

        }

      

        class teste : IHandleEvent
        {
            public Task HandleEventAsync(EventCallbackWorkItem item, object arg)
            {
                throw new NotImplementedException();
            }
        }

    }
    
}

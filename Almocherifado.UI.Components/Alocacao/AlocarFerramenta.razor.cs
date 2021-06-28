using AlmocharifadoApplication;
using Almocherifado.UI.Components.Forms;
using Almocherifado.UI.Components.Models;
using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Almocherifado.UI.Components.Alocacao
{
    public partial class AlocarFerramenta : FormBase
    {

        Toast toast;

        string _teste;
        string teste
        {
            get
            {
                return _teste;
            }
            set
            {
                OnFuncionarioSelect(value);
                _teste = value;
            }
        }
        [Inject] IAlmocharifadoRepository repo { get; set; }
        [Inject] IFerramentaRepository ferramentaRepo { get; set; }
        IEnumerable<Funcionario> Funcionarios = new List<Funcionario>();

        CadastroAlocacaoModel alocacaoInput { get; set; } = new() { Data = DateTime.Today };

        protected override void OnInitialized()
        {
            Funcionarios = repo.GetAllFuncionarios();
        }

        protected void OnFuncionarioSelect(string cpf)
        {
            var selected = Funcionarios.Single(f => f.CPF == cpf);
            alocacaoInput.Responsavel = selected;
        }


        protected override async void OnSubmitAsync()
        {
            base.OnSubmitAsync();

            alocacaoInput.Ferramentas = FerramentasSelecionadas;

            if (form.EditContext.Validate())
            {
                var alocacao = mapper.Map<Entities.Alocacao>(alocacaoInput);
                try
                {
                    repo.SalvarAlocacao(alocacao);
                    alocacaoInput = new() { Data = DateTime.Today };
                    FerramentasSelecionadas.Clear();
                    formClass = formClass.Replace("was-validated", "");
                    toast.Show("Ferramenta(s) alocada(s) correetamente", "sucesso");
                }
                catch (Exception ex)
                {
                    toast.Show(ex.Message, "Erro");
                }
                
            }

        }

        HashSet<Ferramenta> FerramentasSelecionadas { get; set; } = new HashSet<Ferramenta>();
    }
}

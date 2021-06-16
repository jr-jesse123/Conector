using AlmocharifadoApplication;
using Almocherifado.UI.Components.Forms;
using Almocherifado.UI.Components.Models;
using Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Alocacao
{
    public partial class AlocarFerramenta : FormBase
    {
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
            alocacaoInput.Responsavel = Funcionarios.Single(f => f.CPF == (string)cpf);
        }


        protected override void OnSubmit()
        {
            base.OnSubmit();

            alocacaoInput.Ferramentas = FerramentasSelecionadas;

            if (form.EditContext.Validate())
            {
                var alocacao = mapper.Map<Entities.Alocacao>(alocacaoInput);

                repo.SalvarAlocacao(alocacao);
            } 

        }

        List<Ferramenta> FerramentasSelecionadas { get; set; } = new List<Ferramenta>();
    }
}

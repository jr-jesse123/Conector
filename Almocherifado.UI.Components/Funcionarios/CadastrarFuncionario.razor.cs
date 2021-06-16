﻿using Almocherifado.UI.Components.Forms;
using Almocherifado.UI.Components.Helpers;
using Almocherifado.UI.Components.Models;
using Entities;
using Syncfusion.Blazor.Inputs;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;

namespace Almocherifado.UI.Components.Funcionarios
{
    public partial class CadastrarFuncionario : FormBase
    {
        CadastroFuncionarioModel FuncionarioInput = new();

        private List<string> cargos = new()
        {
        "Nenhum ", "Analista administrativo", "Auxiliar administrativo", "Auxilidar de mecânico", "Eletrotécnico",
        "Encarregado de manutenção", "Encarregado de manutenção", "Engenheiro mecânico", "Mecânico de ar condicionado",
        "Mecânico de ar condicionado sênior", "Operador"
        };

        public void ChangeFotos(IEnumerable<UploadFiles> fotos)
        {
            FuncionarioInput.Foto = fotos.First();
        }
        public EditForm Form { get; private set; }

        protected override void OnSubmit()
        {
            base.OnSubmit();

            if (form.EditContext.Validate())
            {
                var funcionarioDomain = mapper.Map<Funcionario>(FuncionarioInput);

                FileHelper.SaveFileToRoot(FuncionarioInput.Foto.Stream, FuncionarioInput.Foto.FileInfo.Name);

                repo.SalvarFuncionario(funcionarioDomain);
            }
            
        }

    }
}

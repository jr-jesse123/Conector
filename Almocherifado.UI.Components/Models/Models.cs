using System;
using System.Collections.Generic;
using Almocherifado.UI.Components.Models;
using FluentValidation;
using Syncfusion.Blazor.Inputs;

namespace Almocherifado.UI.Components.Models
{
    public record CadastroFerramentaModel
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime DataDaCompra { get; set; }
        public int Patrimonio { get; set; }
        public IEnumerable<UploadFiles> Fotos { get; set; }
        public string Descricao { get; set; }
    }
}

namespace Almocherifado.UI.Components
{
    public class FerramentaValidator : AbstractValidator<CadastroFerramentaModel>
    {
        public FerramentaValidator() 
        {
            RuleFor(f => f.Nome).NotEmpty();
            RuleFor(f => f.Marca).NotEmpty();
            RuleFor(f => f.Modelo).NotEmpty();
            RuleFor(f => f.DataDaCompra)
                .InclusiveBetween(new DateTime(1990, 01, 01), DateTime.Now.Date);
            RuleFor(f => f.Fotos).NotEmpty().WithMessage("É preciso adicionar ao menos uma foto");

        }
    }
}
using System;
using Almocherifado.UI.Components.Models;
using FluentValidation;

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
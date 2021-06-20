using System;
using FluentValidation;

namespace Almocherifado.UI.Components.Models
{
    public class CadastroAlocacaoModelValidator : AbstractValidator<CadastroAlocacaoModel>
    {
        public CadastroAlocacaoModelValidator()
        {
            RuleFor(aloc => aloc.Ferramentas).NotEmpty();
            RuleFor(aloc => aloc.Responsavel).NotEmpty();
            RuleFor(aloc => aloc.ContratoLocacao).NotEmpty();
            RuleFor(aloc => aloc.Data).InclusiveBetween(new DateTime(1990, 1, 1), DateTime.Today);
        }
    }

}

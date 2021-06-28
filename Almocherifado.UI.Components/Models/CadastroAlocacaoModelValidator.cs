using System;
using FluentValidation;

namespace Almocherifado.UI.Components.Models
{
    public class CadastroAlocacaoModelValidator : AbstractValidator<CadastroAlocacaoModel>
    {
        public CadastroAlocacaoModelValidator()
        {
            RuleFor(aloc => aloc.Ferramentas).NotNull().NotEmpty().WithMessage("Precisamos de ao menos uma ferramenta para realizar uma alocação");
            RuleFor(aloc => aloc.Responsavel).NotNull().NotEmpty().WithMessage("Informe o responsável pelas ferramentas");
            RuleFor(aloc => aloc.ContratoLocacao).NotNull().NotEmpty();
            RuleFor(aloc => aloc.Data).InclusiveBetween(new DateTime(1990, 1, 1), DateTime.Today);
        }
    }

}

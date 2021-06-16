using FluentValidation;

namespace Almocherifado.UI.Components.Models
{
    public class FuncionarioValidator : AbstractValidator<CadastroFuncionarioModel>
    {
        public FuncionarioValidator()
        {
            RuleFor(f => f.Nome).NotEmpty();
            RuleFor(f => f.CPF).IsValidCPF() ;
            RuleFor(f => f.Cargo).NotEmpty();
            RuleFor(f => f.Email).NotEmpty().EmailAddress();
            RuleFor(f => f.Foto).NotEmpty();
        }
    }

}

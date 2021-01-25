using Almocherifado.core.AgregateRoots.FuncionarioNm;
using CSharpFunctionalExtensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Data.Models
{
    public class FuncionarioModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }


        public Funcionario GetFuncionario()
        {
            var failures = new FuncionarioValidator().Validate(this);

            if (failures.Errors.Count == 0)
            {
                return new Funcionario(Nome, CPF, Email);
            }
            else
            {
                throw new Exception(failures.Errors.First().ErrorMessage);
            }
        }
    }


    public class FuncionarioValidator : AbstractValidator<FuncionarioModel>
    {
        public FuncionarioValidator()
        {
            RuleFor(f => f.Nome).Must(SerNomeValido).WithMessage(MensagemValidaCaoNome);
            RuleFor(f => f.CPF).Must(cpfStr => CPF.Create(cpfStr).IsSuccess).WithMessage(model => CPF.Create(model.CPF).Error);
            RuleFor(f => f.Email).Must(emailStr => Email.Create(emailStr).IsSuccess).WithMessage(model => Email.Create(model.CPF).Error);
        }

        private bool SerNomeValido(string arg) =>
            Nome.Create(arg).IsSuccess ;
        private string MensagemValidaCaoNome(FuncionarioModel model) =>
            Nome.Create(model.Nome).Error;

    }

}

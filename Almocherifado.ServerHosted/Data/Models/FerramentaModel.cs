using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Data.Models
{
    public class FerramentaModel
    {
        public string NomeAbreviado { get; set; }
        public string Descrição { get; set; }
        public DateTime DataCompra { get; set; }
        public string FotoUrl { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
    }


    public class FerramentaValidator : AbstractValidator<FerramentaModel>
    {
        public FerramentaValidator()
        {
            RuleFor(f => f.NomeAbreviado)
                .NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(f => f.Descrição)
                .NotEmpty().WithMessage("A descrição é obrigatória");

            RuleFor(f => f.DataCompra)
                .GreaterThan(DateTime.Now.AddYears(-10))
                .WithMessage("A ferramenta não pode ser mais velha do que 10 anos")
                .LessThan(DateTime.Today.AddDays(1)).WithMessage("A data de compra da ferramenta é posterior ao dia de hoje");

            RuleFor(f => f.FotoUrl).NotEmpty().WithMessage("A foto é obrigatória");
        }
    }

}

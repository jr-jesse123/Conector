using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Models
{
    public record DevolucaoInputModel
    {
        public Dictionary<Ferramenta, string> FerramentasEComentarios { get; set; }
    }

    public class DevolucaoInputvalidator : AbstractValidator<DevolucaoInputModel>
    {
        public DevolucaoInputvalidator()
        {
            RuleFor(dim => dim.FerramentasEComentarios).NotEmpty();
        }
    }
}

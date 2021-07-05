﻿using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almocherifado.UI.Components.Models
{
    public class FerramentaDevolucaoInputModel
    {
        public FerramentaDevolucaoInputModel(Ferramenta ferramenta, string comentario)
        {
            Ferramenta = ferramenta;
            Comentario = comentario;
        }

        public Ferramenta Ferramenta { get; set; }
        public string Comentario { get; set; }

    }
    public record DevolucaoInputModel
    {
        public List<FerramentaDevolucaoInputModel> FerramentasEComentarios { get; set; }
    }

    public class DevolucaoInputvalidator : AbstractValidator<DevolucaoInputModel>
    {
        public DevolucaoInputvalidator()
        {
            RuleFor(dim => dim.FerramentasEComentarios).NotEmpty();
        }
    }
}

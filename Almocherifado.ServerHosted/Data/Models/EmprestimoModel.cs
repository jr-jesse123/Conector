﻿using Almocherifado.core.Entitys;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Data.Models
{
    public class EmprestimoModel
    {
        public DateTime Entrega { get; set; }
        public string FuncionarioCpf { get; set; }
        public List<Ferramenta> Ferramentas { get; set; }
        public string   Obra { get; set; }
        public string TermoResponsabilidade { get; set; }
    }


    public class EmprestimoValidator : AbstractValidator<EmprestimoModel>
    {
        public EmprestimoValidator()
        {
            RuleFor(e => e.Entrega).InclusiveBetween(DateTime.Now.AddDays(-7), DateTime.Now)
                .WithMessage("A data da entrega precisa estar dentro dos últimos 7 dias");

            RuleFor(e => e.FuncionarioCpf).NotNull().WithMessage("Escolha a quem a(s) ferramenta(s) foram entregues");

            RuleFor(e => e.Obra).NotEmpty().WithMessage("É necessário informar em qual obra/locação as ferramentas serão utilizadas");

            RuleFor(e => e.Ferramentas).NotNull().NotEmpty().WithMessage("Informe as ferramentas que serão alocadas");

            RuleFor(e => e.TermoResponsabilidade).NotNull().NotEmpty().WithMessage("Adicione imagem do termo de responsabilidade");

        }
    }

    //TODO: CONTINUAR CRIANDO A VALIDAÇÃO DE EMPRÉSTIMO´E O FORMULÁRIO PARA ADICIONAR NOVO EMPRESTIMO

}
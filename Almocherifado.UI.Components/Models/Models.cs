using System;
using System.Collections.Generic;
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
    
    public enum Cargo
    {
        Nenhum,
        AnalistaAdministrativo,
        AuxiliarAdministrativo,
        AuxiliarDeMeMecanico,
        EletroTecncnico,
        EncarregadoDeManutencao,
        EngenhoeiroMecanico,
        MecanicoDeArCondicionado,
        MecanicoDeArCondicionadoSenior,
        Operador
    }

    public record CadastroFuncionarioModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public UploadFiles Foto { get; set; }
    }

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

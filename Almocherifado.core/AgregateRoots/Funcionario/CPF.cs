using CpfLibrary;
using CSharpFunctionalExtensions;
using System;

namespace Almocherifado.core.AgregateRoots.FuncionarioNm
{
    public sealed class CPF : ValueObject<CPF>
    {
        public string Value { get; }

        private CPF(string value)
        {
            Value = value;
        }

        private CPF() { }

        public static implicit operator string(CPF Cpf) => Cpf.Value;



        public static implicit operator CPF(string CpfStr)
        {
            return Create(CpfStr).Value;
        }


        public static Result<CPF> Create(string cpfStr)
        {
            if (string.IsNullOrWhiteSpace(cpfStr))
                return Result.Failure<CPF>("CPF não pode ser vazio");

            if (!Cpf.Check(cpfStr))
                return Result.Failure<CPF>("Número de cpf Inválido");


            return new CPF(cpfStr.SomenteNumeros());
        }


        protected override bool EqualsCore(CPF other) => other.Value == Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();

        public override string ToString() => Cpf.Format(Value);

    }




}

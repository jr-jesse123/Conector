using CSharpFunctionalExtensions;
using System;

namespace Almocherifado.core.AgregateRoots.FuncionarioNm
{
    public class Email : ValueObject<Email>
    {
        public string Value { get; }

        private Email(string emailStr = null)
        {
            Value = emailStr;
        }

        private Email() { }

        public static Result<Email> Create(string emailStr)
        {
            if (string.IsNullOrWhiteSpace(emailStr))
                return Result.Failure<Email>("Email não pode ser vazio");
            if (!emailStr.Contains("@"))
                return Result.Failure<Email>("Emails devem conter @");
            if (!emailStr.Contains("."))
                return Result.Failure<Email>("Emails deve conter '.'");
            if (!(emailStr.Length > 8))
                return Result.Failure<Email>("O email é muito curto");

            return new Email(emailStr);

        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string emailStr) => new Email(emailStr);

        protected override bool EqualsCore(Email other) => other.Value == Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();


        public override string ToString()
        {
            return Value;
        }
    }


}

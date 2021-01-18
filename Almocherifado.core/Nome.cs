using CSharpFunctionalExtensions;
using static CSharpFunctionalExtensions.Result;

namespace Almocherifado.core
{
    public sealed class Nome : ValueObject<Nome>
    {
        public string Value;
        
        private Nome() { }

        private Nome(string nomeStr)
        {
            Value = nomeStr;
        }
        public static implicit operator string(Nome nome) => nome.Value;
        public static implicit operator Nome(string nomestr) => Create(nomestr).Value;

        public static Result<Nome> Create(string NomeStr)
        {
            if (string.IsNullOrWhiteSpace(NomeStr))
                return Failure<Nome>("Nome não pode ser fazio");
            
            if (NomeStr.Split(' ').Length < 2)
                return Failure<Nome>("Insira o nome Completo");
            
            return new Nome(NomeStr);
        }

        protected override bool EqualsCore(Nome other)
        => other.Value == Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();
        public override string ToString()
        {
            return Value;
        }

        
    }

    


}

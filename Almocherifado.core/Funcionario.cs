using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;

namespace Almocherifado.core
{
    public sealed  class Funcionario : ValueObject<Funcionario>
    {
        public Nome Nome { get; }

        private string _cpf { get; }
        public CPF CPF { get; }
        public Email Email { get; }
        public Funcionario(Nome nome, CPF cpf, Email email)
        {
            Nome = nome;
            this.CPF = cpf;
            _cpf = cpf;
            Email = email;
        }
        private Funcionario() { }

        protected override bool EqualsCore(Funcionario other) => other.CPF == CPF;

        protected override int GetHashCodeCore() => CPF is not null ? CPF.GetHashCode() : 00000000;
        public override string ToString()
        {
            return Nome + ";" + CPF + ";" + Email;
        }

    }


}

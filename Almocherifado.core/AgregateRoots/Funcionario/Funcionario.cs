using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Almocherifado.core.AgregateRoots.FuncionarioNm
{
    public class Funcionario : ValueObject<Funcionario>
    {
        public Nome Nome { get; }
        public CPF CPF { get; }
        public Email Email { get; }
        public Funcionario(Nome nome, CPF cpf, Email email)
        {
            Nome = nome;
            CPF = cpf;
            
            Email = email;
        }
        protected Funcionario() { }

        protected override bool EqualsCore(Funcionario other) => other.CPF == CPF;

        protected override int GetHashCodeCore() => CPF is not null ? CPF.GetHashCode() : 00000000;
        public override string ToString()
        {
            return Nome + ";" + CPF + ";" + Email;
        }

    }


}

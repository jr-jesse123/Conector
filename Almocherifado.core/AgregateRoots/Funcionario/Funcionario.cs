using CSharpFunctionalExtensions;

namespace Almocherifado.core.AgregateRoots.FuncionarioNm
{
    public class Funcionario : Entity
    {
        public Nome Nome { get; }
        public CPF CPF { get; }
        public Email Email { get; }

        //public Funcionario( CPF cpf)
        //{

        //    CPF = cpf;


        //}


        public Funcionario(Nome nome, CPF cpf, Email email)
        {
            
            Nome = nome;
            CPF = cpf;
            
            Email = email;
        }
        protected Funcionario() { }

        public override string ToString()
        {
            return Nome + ";" + CPF + ";" + Email;
        }
    }


}

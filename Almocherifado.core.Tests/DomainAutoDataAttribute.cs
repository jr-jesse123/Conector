using Almocherifado.core.AgregateRoots.FuncionarioNm;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Bogus;
using Bogus.Extensions.Brazil;
using System;

namespace Almocherifado.core.Tests
{

    public class DomainFixture : Fixture
    {
        public DomainFixture()
        {
            Customizations.Add(new DomainClassesGenerator());
        }
    }

    public class DomainAutoDataAttribute : AutoDataAttribute
    {
        public DomainAutoDataAttribute() : base(Customizar())
        {

        }

        private static Func<IFixture> Customizar()
        {
            return () => new DomainFixture();
        }
    }

}

class DomainClassesGenerator : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        Type type = request as Type;
        if (type is null)
        {
            return new NoSpecimen();
        }

        if (type == typeof(Nome))
        {
            return (Nome)new Faker("pt_BR").Person.FullName;
        }

        if (type == typeof(CPF))
        {
            return (CPF)new Faker("pt_BR").Person.Cpf();
        }

        if (type == typeof(Email))
        {
            return (Email)new Faker("pt_BR").Person.Email;
        }

        return new NoSpecimen();

    }
}





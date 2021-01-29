using Almocherifado.core.AgregateRoots.EmprestimoNm;
using Almocherifado.core.AgregateRoots.FuncionarioNm;
using Almocherifado.core.Services;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Reflection;

namespace Almocherifado.core.Tests
{

 


    public class DomainFixture : Fixture
    {
        public DomainFixture()
        {
            Customizations.Add(new DomainClassesGenerator());
            Customizations.Add(  new TypeRelay(
        typeof(Almocherifado.core.Services.IModeloTermoService),
        typeof(ModeloTermoService)));
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

        PropertyInfo propertyInfo = request as PropertyInfo;
        
        if (propertyInfo is not null)
        {
            if (propertyInfo.Name == "entrega" && propertyInfo.PropertyType == typeof(DateTime))
            {
                return DateTime.Today;
            }
        }

        return new NoSpecimen();

    }
}





using Almocherifado.ServerHosted.Data.Models;
using AutoFixture.Kernel;
using Bogus;
using System;
using Bogus.Extensions.Brazil;
using System.Reflection;

namespace Almocherifado.ServerHosted.Tests
{
    public partial class Mappingtests
    {
        class UIClassGenerator : ISpecimenBuilder
        {
            public object Create(object request, ISpecimenContext context)
            {
                Type type = request as Type;
                if (type is null)
                {
                    return new NoSpecimen();
                }

                if (type == typeof(FuncionarioModel))
                {
                    var faker = new Faker<FuncionarioModel>()
                        
                        .RuleFor(f => f.Nome, eh => eh.Person.FullName)
                        .RuleFor(f => f.Email, eh => eh.Person.Email);

                    var fake= faker.Generate();
                    fake.CPF = new Faker().Person.Cpf();
                    return fake;
                }


                //PropertyInfo Prop = request as PropertyInfo;
                //if (Prop?.Name == "Id")
                //{
                //    return new Random().Next(1,10000);
                //}

                return new NoSpecimen();

            }

            

        }


    


    }
    
}

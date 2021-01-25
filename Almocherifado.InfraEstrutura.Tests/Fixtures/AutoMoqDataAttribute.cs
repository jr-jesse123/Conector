using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;

namespace Almocherifado.InfraEstrutura.Tests.Fixtures
{
    [AttributeUsage( AttributeTargets.Method)]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(NewMethod())
        {

        }

        private static Func<IFixture> NewMethod()
        {
            return () => new Fixture().Customize(new AutoMoqCustomization() );
        }
    }
}

using Almocherifado.core.Tests;
using AutoFixture.AutoMoq;

namespace Almocherifado.UI.Tests
{
        public class  UIFixture : DomainFixture
        {
            public UIFixture()
            {
                Customizations.Add(new UIClassGenerator());
                Customize(new AutoMoqCustomization() );
                
            }
        }


}

using Almocherifado.core.Tests;
using AutoFixture.AutoMoq;

namespace Almocherifado.ServerHosted.Tests
{
    public partial class Mappingtests
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
    
}

using AutoFixture.Xunit2;

namespace Almocherifado.ServerHosted.Tests
{
    public partial class Mappingtests
    {
        public class UiAutoData : AutoDataAttribute
        {
            public UiAutoData() : base( () => new UIFixture())
            {

            }
        }


    


    }
    
}

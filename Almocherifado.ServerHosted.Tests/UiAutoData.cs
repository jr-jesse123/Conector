
using AutoFixture.Xunit2;

namespace Almocherifado.UI.Tests
{
    public class UiAutoData : AutoDataAttribute
        {
            public UiAutoData() : base( () => new UIFixture())
            {

            }
        }
}

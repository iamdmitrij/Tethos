using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit2;

namespace Tethos.FakeItEasy.Tests.Attributes
{
    internal class FactoryContainerDataAttribute : AutoDataAttribute
    {
        public FactoryContainerDataAttribute() : base(
            () =>
            {
                var fixture = new Fixture();
                fixture.Register(AutoFakeItEasyContainerFactory.Create);
                return fixture.Customize(new AutoFakeItEasyCustomization());
            }
        )
        {
        }
    }
}

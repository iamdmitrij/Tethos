using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Tethos.Moq.Tests.Attributes
{
    internal class FactoryContainerDataAttribute : AutoDataAttribute
    {
        public FactoryContainerDataAttribute() : base(
            () =>
            {
                var fixture = new Fixture();
                fixture.Register(AutoMoqContainerFactory.Create);
                return fixture.Customize(new AutoMoqCustomization());
            })
        {
        }
    }
}

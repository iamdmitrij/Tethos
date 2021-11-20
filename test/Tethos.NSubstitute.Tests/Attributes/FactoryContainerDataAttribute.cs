using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Tethos.NSubstitute.Tests.Attributes
{
    internal class FactoryContainerDataAttribute : AutoDataAttribute
    {
        public FactoryContainerDataAttribute()
            : base(
            () =>
            {
                var fixture = new Fixture();
                fixture.Register(AutoNSubstituteContainerFactory.Create);
                return fixture.Customize(new AutoNSubstituteCustomization());
            })
        {
        }
    }
}

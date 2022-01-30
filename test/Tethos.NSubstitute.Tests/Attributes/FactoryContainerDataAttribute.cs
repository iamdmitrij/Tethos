namespace Tethos.NSubstitute.Tests.Attributes
{
    using AutoFixture;
    using AutoFixture.Xunit2;

    internal class FactoryContainerDataAttribute : AutoDataAttribute
    {
        public FactoryContainerDataAttribute()
            : base(
            () =>
            {
                var fixture = new Fixture();
#pragma warning disable CS0618 // Type or member is obsolete
                fixture.Register(AutoMockingContainerFactory.Create);
#pragma warning restore CS0618 // Type or member is obsolete
                return fixture;
            })
        {
        }
    }
}

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
                fixture.Register(NSubstitute.AutoMockingContainerFactory.Create);
                return fixture;
            })
        {
        }
    }
}

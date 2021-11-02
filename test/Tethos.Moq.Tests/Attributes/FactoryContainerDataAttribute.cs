namespace Tethos.Moq.Tests.Attributes
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Xunit2;

    internal class FactoryContainerDataAttribute : AutoDataAttribute
    {
        public FactoryContainerDataAttribute() : base(
            () =>
            {
                var fixture = new Fixture();
                fixture.Register(AutoMoqContainerFactory.Create);
                return fixture.Customize(new AutoMoqCustomization());
            }
        )
        {
        }
    }
}

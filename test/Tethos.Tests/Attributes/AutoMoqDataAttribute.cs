namespace Tethos.Tests.Attributes
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Xunit2;

    internal class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(
            () => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}

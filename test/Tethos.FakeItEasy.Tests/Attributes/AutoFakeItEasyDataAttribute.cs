namespace Tethos.NSubstitute.Tests.Attributes
{
    using AutoFixture;
    using AutoFixture.AutoFakeItEasy;
    using AutoFixture.Xunit2;

    internal class AutoFakeItEasyDataAttribute : AutoDataAttribute
    {
        public AutoFakeItEasyDataAttribute() : base(
            () => new Fixture().Customize(new AutoFakeItEasyCustomization())
        )
        {
        }
    }
}

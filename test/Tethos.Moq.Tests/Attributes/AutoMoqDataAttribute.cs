using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Tethos.NSubstitute.Tests.Attributes
{
    internal class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(
            () => new Fixture().Customize(new AutoMoqCustomization())
        )
        {
        }
    }
}

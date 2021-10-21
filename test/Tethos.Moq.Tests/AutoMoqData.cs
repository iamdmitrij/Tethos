using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Tethos.NSubstitute.Tests
{
    internal class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData() : base(
            () => new Fixture().Customize(new AutoMoqCustomization())
        )
        {
        }
    }
}

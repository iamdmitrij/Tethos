using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Tethos.NSubstitute.Tests
{
    internal class AutoNSubstituteData : AutoDataAttribute
    {
        internal AutoNSubstituteData() : base(
            () => new Fixture().Customize(new AutoNSubstituteCustomization())
        )
        {
        }
    }
}

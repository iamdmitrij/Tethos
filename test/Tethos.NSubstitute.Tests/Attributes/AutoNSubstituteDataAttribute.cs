using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Tethos.NSubstitute.Tests.Attributes
{
    internal class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(
            () => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}

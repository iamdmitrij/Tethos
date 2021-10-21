using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit2;

namespace Tethos.NSubstitute.Tests
{
    internal class AutoFakeItEasyData : AutoDataAttribute
    {
        internal AutoFakeItEasyData() : base(
            () => new Fixture().Customize(new AutoFakeItEasyCustomization())
        )
        {
        }
    }
}

using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit2;

namespace Tethos.FakeItEasy.Tests.Attributes
{
    internal class AutoFakeItEasyDataAttribute : AutoDataAttribute
    {
        public AutoFakeItEasyDataAttribute() : base(
            () => new Fixture().Customize(new AutoFakeItEasyCustomization()))
        {
        }
    }
}

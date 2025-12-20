namespace Tethos.FakeItEasy.Tests.Attributes;

using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit3;

internal class AutoFakeItEasyDataAttribute : AutoDataAttribute
{
    public AutoFakeItEasyDataAttribute()
        : base(
        () => new Fixture().Customize(new AutoFakeItEasyCustomization()))
    {
    }
}

namespace Tethos.NSubstitute.Tests.Attributes;

using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit3;

internal class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
        : base(
        () => new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
    }
}

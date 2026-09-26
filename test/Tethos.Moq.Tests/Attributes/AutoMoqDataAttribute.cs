namespace Tethos.Moq.Tests.Attributes;

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit3;

internal class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(
        () => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}

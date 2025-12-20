namespace Tethos.Tests.Attributes;

using AutoFixture.Xunit3;

internal class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] objects)
        : base(new AutoMoqDataAttribute(), objects)
    {
    }
}

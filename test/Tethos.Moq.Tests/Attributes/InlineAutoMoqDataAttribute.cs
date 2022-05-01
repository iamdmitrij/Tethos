namespace Tethos.Moq.Tests.Attributes;

using AutoFixture.Xunit2;

internal class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] objects)
        : base(new AutoMoqDataAttribute(), objects)
    {
    }
}


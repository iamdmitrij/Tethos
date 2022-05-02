namespace Tethos.Tests.Attributes;

using AutoFixture.Xunit2;
using Tethos.NSubstitute.Tests.Attributes;

internal class InlineAutoNSubstituteDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] objects)
        : base(new AutoNSubstituteDataAttribute(), objects)
    {
    }
}

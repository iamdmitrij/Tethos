namespace Tethos.Tests.Attributes;

using AutoFixture.Xunit3;
using Tethos.NSubstitute.Tests.Attributes;

internal class InlineAutoNSubstituteDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] objects)
        : base(new AutoNSubstituteDataAttribute(), objects)
    {
    }
}

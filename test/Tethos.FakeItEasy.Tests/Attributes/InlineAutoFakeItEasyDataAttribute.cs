namespace Tethos.Tests.Attributes;

using AutoFixture.Xunit3;
using Tethos.FakeItEasy.Tests.Attributes;

internal class InlineAutoFakeItEasyDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoFakeItEasyDataAttribute(params object[] objects)
        : base(new AutoFakeItEasyDataAttribute(), objects)
    {
    }
}

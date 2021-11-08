using AutoFixture.Xunit2;

namespace Tethos.Tests.Attributes
{
    internal class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects)
            : base(new AutoMoqDataAttribute(), objects)
        {
        }
    }
}

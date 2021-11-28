namespace Tethos.Tests.Common
{
    internal class InternalSystemUnderTest
    {
        public InternalSystemUnderTest(IMockable mockable) => this.Mockable = mockable;

        public IMockable Mockable { get; }

        public int Do() => this.Mockable.Do();
    }
}

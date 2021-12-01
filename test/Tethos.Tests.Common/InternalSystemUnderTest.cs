namespace Tethos.Tests.Common
{
    internal class InternalSystemUnderTest
    {
        public InternalSystemUnderTest(IMockable mockable) => this.Mockable = mockable;

        public IMockable Mockable { get; }

        public int Exercise() => this.Mockable.Get();
    }
}

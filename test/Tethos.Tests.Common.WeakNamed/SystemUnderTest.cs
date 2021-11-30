namespace Tethos.Tests.Common.WeakNamed
{
    internal class SystemUnderTest
    {
        public SystemUnderTest(IMockable mockable) => this.Mockable = mockable;

        public IMockable Mockable { get; }

        public int Exercise() => this.Mockable.Get();
    }
}

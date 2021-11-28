namespace Tethos.Tests.Common
{
    internal class SystemUnderTestWithInternal
    {
        public SystemUnderTestWithInternal(IInternalMockable mockable) => this.Mockable = mockable;

        public IInternalMockable Mockable { get; }

        public int Do() => this.Mockable.Do();
    }
}

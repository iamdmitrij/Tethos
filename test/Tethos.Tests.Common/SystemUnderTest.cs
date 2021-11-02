namespace Tethos.Tests.Common
{
    public class SystemUnderTest
    {
        public SystemUnderTest(IMockable mockable) => this.Mockable = mockable;

        public IMockable Mockable { get; }

        public int Do() => this.Mockable.Do();
    }
}

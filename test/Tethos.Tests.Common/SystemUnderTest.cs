namespace Tethos.Tests.Common
{
    public class SystemUnderTest
    {
        public IMockable Mockable { get; }

        public SystemUnderTest(IMockable mockable) => this.Mockable = mockable;

        public int Do() => this.Mockable.Do();
    }
}

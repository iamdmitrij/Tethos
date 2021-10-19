namespace Tethos.Tests.Common
{
    public class SystemUnderTest
    {
        public IMockable Mockable { get; }

        public SystemUnderTest(IMockable mockable)
        {
            Mockable = mockable;
        }

        public int Do() => Mockable.Do();
    }
}

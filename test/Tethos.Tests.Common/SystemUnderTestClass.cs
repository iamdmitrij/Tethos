namespace Tethos.Tests.Common
{
    public class SystemUnderTestClass
    {
        public Concrete Mockable { get; }

        public SystemUnderTestClass(Concrete mockable) => Mockable = mockable;

        public int Do() => Mockable.Do();
    }
}

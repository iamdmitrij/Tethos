namespace Tethos.Tests.Common
{
    public class SystemUnderTestClass
    {
        public Concrete Mockable { get; }

        public SystemUnderTestClass(Concrete mockable) => this.Mockable = mockable;

        public int Do() => this.Mockable.Do();
    }
}

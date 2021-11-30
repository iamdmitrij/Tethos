namespace Tethos.Tests.Common
{
    public class SystemUnderTestClass
    {
        public SystemUnderTestClass(Concrete mockable) => this.Mockable = mockable;

        public Concrete Mockable { get; }

        public int Exercise() => this.Mockable.Get();
    }
}

namespace Tethos.Moq.Tests.SUT
{
    using global::Moq;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest()
        {
            this.Proxy = new Mock<AutoMoqContainer>();
            this.Container = this.Proxy.Object;
        }

        public Mock<AutoMoqContainer> Proxy { get; }
    }
}

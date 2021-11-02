namespace Tethos.Moq.Tests.SUT
{
    using Moq;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public Mock<AutoMoqContainer> Proxy { get; }

        public InheritedAutoMockingTest()
        {
            this.Proxy = new Mock<AutoMoqContainer>();
            this.Container = this.Proxy.Object;
        }
    }
}

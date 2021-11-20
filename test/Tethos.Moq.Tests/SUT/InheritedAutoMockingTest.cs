using Moq;

namespace Tethos.Moq.Tests.SUT
{
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

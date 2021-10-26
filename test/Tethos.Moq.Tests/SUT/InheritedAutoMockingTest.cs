using Moq;

namespace Tethos.Moq.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public Mock<AutoMoqContainer> Proxy { get; }

        public InheritedAutoMockingTest()
        {
            Proxy = new Mock<AutoMoqContainer>();
            Container = Proxy.Object;
        }
    }
}

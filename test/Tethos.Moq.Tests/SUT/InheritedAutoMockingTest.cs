using Moq;

namespace Tethos.Moq.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public Mock<AutoMoqContainer> ContainerMock { get; }

        public InheritedAutoMockingTest()
        {
            ContainerMock = new Mock<AutoMoqContainer>();
            Container = ContainerMock.Object;
        }
    }
}

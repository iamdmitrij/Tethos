using Moq;

namespace Tethos.Moq.Tests.SUT
{
    internal class InheritedAutoMockingTest : AutoMockingTest
    {
        public Mock<AutoMoqContainer> ContainerMock { get; set; }

        public InheritedAutoMockingTest()
        {
            ContainerMock = new Mock<AutoMoqContainer>();
            Container = ContainerMock.Object;
        }
    }
}

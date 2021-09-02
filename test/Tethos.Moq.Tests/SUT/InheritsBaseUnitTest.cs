using Moq;

namespace Tethos.Moq.Tests.SUT
{
    public class InheritsBaseUnitTest : AutoMockingTest
    {
        public Mock<AutoMoqContainer> ContainerMock { get; set; }

        public InheritsBaseUnitTest()
        {
            ContainerMock = new Mock<AutoMoqContainer>();
            Container = ContainerMock.Object;
        }
    }
}

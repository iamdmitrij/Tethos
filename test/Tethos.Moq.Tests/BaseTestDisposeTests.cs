using Moq;
using Xunit;
using Tethos.Moq.Tests.SUT;

namespace Tethos.Moq.Tests
{
    public class BaseTestDisposeTests : AutoMockingTest
    {
        [Fact]
        public void InheritsBaseUnitTest_Dispose_ShouldDisposeWindsorContainer()
        {
            // Arrange
            var sut = new InheritsBaseUnitTest();
            
            // Act
            sut.Dispose();

            // Assert
            sut.ContainerMock.Verify(x => x.Dispose(), Times.Once);
        }
    }
}

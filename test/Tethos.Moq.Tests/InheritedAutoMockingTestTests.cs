using Moq;
using Xunit;
using Tethos.Moq.Tests.SUT;

namespace Tethos.Moq.Tests
{
    public class InheritedAutoMockingTestTests : AutoMockingTest
    {
        [Fact]
        public void InheritedAutoMockingTestTests_Dispose_ShouldDisposeWindsorContainer()
        {
            // Arrange
            var sut = new InheritedAutoMockingTest();
            
            // Act
            sut.Dispose();

            // Assert
            sut.ContainerMock.Verify(x => x.Dispose(), Times.Once);
        }
    }
}

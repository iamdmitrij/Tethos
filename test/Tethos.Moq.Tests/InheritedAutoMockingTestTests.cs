using Moq;
using Xunit;
using Tethos.Moq.Tests.SUT;
using AutoFixture.Xunit2;

namespace Tethos.Moq.Tests
{
    public class InheritedAutoMockingTestTests : AutoMockingTest
    {
        [Theory, AutoData]
        public void Dispose_ShouldDisposeMock(InheritedAutoMockingTest sut)
        {
            // Act
            sut.Dispose();

            // Assert
            sut.Proxy.Verify(x => x.Dispose(), Times.Once);
        }

        [Theory, AutoData]
        public void Dispose_NullContainer_ShouldNotDisposeMock(InheritedAutoMockingTest sut)
        {
            // Arrange
            sut.Container = null;

            // Act
            sut.Dispose();

            // Assert
            sut.Proxy.Verify(x => x.Dispose(), Times.Never);
        }
    }
}

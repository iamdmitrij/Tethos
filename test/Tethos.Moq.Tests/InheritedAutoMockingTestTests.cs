using AutoFixture.Xunit2;
using Moq;
using Tethos.Moq.Tests.SUT;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class InheritedAutoMockingTestTests : AutoMockingTest
    {
        [Theory, AutoData]
        [Trait("", "Unit")]
        public void Dispose_ShouldDisposeMock(InheritedAutoMockingTest sut)
        {
            // Act
            sut.Dispose();

            // Assert
            sut.Proxy.Verify(x => x.Dispose(), Times.Once);
        }

        [Theory, AutoData]
        [Trait("", "Unit")]
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

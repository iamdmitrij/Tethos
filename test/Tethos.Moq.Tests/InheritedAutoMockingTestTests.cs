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
            sut.ContainerMock.Verify(x => x.Dispose(), Times.Once);
        }
    }
}

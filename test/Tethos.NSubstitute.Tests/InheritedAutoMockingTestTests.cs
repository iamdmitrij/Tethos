using Xunit;
using AutoFixture.Xunit2;
using Tethos.NSubstitute.Tests.SUT;

namespace Tethos.NSubstitute.Tests
{
    public class InheritedAutoMockingTestTests : AutoMockingTest
    {
        [Theory, AutoData]
        public void Dispose_ShouldDisposeMock(InheritedAutoMockingTest sut)
        {
            // Act
            sut.Dispose();

            // Assert
            //sut.ContainerMock.Verify(x => x.Dispose(), Times.Once);
        }
    }
}

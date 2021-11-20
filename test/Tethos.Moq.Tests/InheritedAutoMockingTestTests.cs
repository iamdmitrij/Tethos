namespace Tethos.Moq.Tests
{
    using AutoFixture.Xunit2;
    using global::Moq;
    using Tethos.Moq.Tests.SUT;
    using Xunit;

    public class InheritedAutoMockingTestTests : AutoMockingTest
    {
        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
        public void Dispose_ShouldDisposeMock(InheritedAutoMockingTest sut)
        {
            // Act
            sut.Dispose();

            // Assert
            sut.Proxy.Verify(x => x.Dispose(), Times.Once);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
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

using Xunit;
using AutoFixture.Xunit2;
using Tethos.NSubstitute.Tests.SUT;
using NSubstitute;

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
            sut.Container.Received().Dispose();
        }
    }
}

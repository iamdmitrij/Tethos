﻿namespace Tethos.FakeItEasy.Tests
{
    using AutoFixture.Xunit2;
    using global::FakeItEasy;
    using Tethos.FakeItEasy.Tests.SUT;
    using Xunit;

    public class InheritedAutoMockingTestTests
    {
        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
        public void Dispose_ShouldDisposeMock(InheritedAutoMockingTest sut)
        {
            // Act
            sut.Dispose();

            // Assert
            A.CallTo(() => sut.Container.Dispose()).MustHaveHappened();
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
            A.CallTo(() => sut.Proxy.Dispose()).MustNotHaveHappened();
        }
    }
}

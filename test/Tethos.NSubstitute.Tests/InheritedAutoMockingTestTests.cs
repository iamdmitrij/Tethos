﻿using AutoFixture.Xunit2;
using NSubstitute;
using Tethos.NSubstitute.Tests.SUT;
using Xunit;

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

        [Theory, AutoData]
        public void Dispose_NullContainer_ShouldNotDisposeMock(InheritedAutoMockingTest sut)
        {
            // Arrange
            sut.Container = null;

            // Act
            sut.Dispose();

            // Assert
            sut.Proxy.DidNotReceive().Dispose();
        }
    }
}

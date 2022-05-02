﻿namespace Tethos.Moq.Tests.AutoMockingTest.Configuration.IncludeNonPublicTypes;

using Castle.MicroKernel;
using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

public class DefaultNonPublicTypesTests : Moq.AutoMockingTest
{
    [Fact]
    [Trait("Type", "Integration")]
    public void Resolve_WithDefaultIncludeNonPublicTypesConfiguration_ShouldThrowComponentNotFoundException()
    {
        // Arrange
        var sut = () => this.Container.Resolve<InternalSystemUnderTest>();

        // Act & Assert
        sut.Should().Throw<ComponentNotFoundException>();
    }
}

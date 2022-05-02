namespace Tethos.Moq.Tests.AutoMockingTest.Configuration;

using Castle.MicroKernel;
using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

public class PropertyNonPublicTypesDisabledTests : Moq.AutoMockingTest
{
    public override AutoMockingConfiguration AutoMockingConfiguration => new() { IncludeNonPublicTypes = false };

    [Fact]
    [Trait("Type", "Integration")]
    public void Resolve_WithIncludeNonPublicTypesDisabled_ShouldThrowComponentNotFoundException()
    {
        // Arrange
        var sut = () => this.Container.Resolve<InternalSystemUnderTest>();

        // Act & Assert
        sut.Should().Throw<ComponentNotFoundException>();
    }
}

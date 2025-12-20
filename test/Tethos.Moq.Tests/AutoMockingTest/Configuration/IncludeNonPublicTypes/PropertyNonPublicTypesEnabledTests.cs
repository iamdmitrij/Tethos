namespace Tethos.Moq.Tests.AutoMockingTest.Configuration.IncludeNonPublicTypes;

using AutoFixture.Xunit3;
using FluentAssertions;
using global::Moq;
using Tethos.Tests.Common;
using Xunit;

public class PropertyNonPublicTypesEnabledTests : Moq.AutoMockingTest
{
    public override AutoMockingConfiguration AutoMockingConfiguration => new() { IncludeNonPublicTypes = true };

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Resolve_WithIncludeNonPublicTypesEnabled_ShouldMatch(int expected)
    {
        // Arrange
        var sut = this.Container.Resolve<InternalSystemUnderTest>();
        var mock = this.Container.Resolve<Mock<IMockable>>()
            .Setup(m => m.Get())
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }
}

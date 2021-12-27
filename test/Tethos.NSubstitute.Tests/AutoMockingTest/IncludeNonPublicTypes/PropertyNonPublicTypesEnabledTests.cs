namespace Tethos.NSubstitute.Tests.AutoMockingTest.Configuration.IncludeNonPublicTypes
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.Tests.Common;
    using Xunit;

    public class PropertyNonPublicTypesEnabledTests : NSubstitute.AutoMockingTest
    {
        public override AutoMockingConfiguration AutoMockingConfiguration => new() { IncludeNonPublicTypes = true };

        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        public void Resolve_WithIncludeNonPublicTypesEnabled_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<InternalSystemUnderTest>();
            this.Container.Resolve<IMockable>()
                .Get()
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
        }
    }
}

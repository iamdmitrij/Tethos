namespace Tethos.Moq.Tests.AutoMockingTest.Configuration
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Tests.Common;
    using Xunit;

    public class MethodNonPublicTypesEnabledTests : Moq.AutoMockingTest
    {
        public override AutoMockingConfiguration OnConfigurationCreated(AutoMockingConfiguration configuration)
        {
            configuration.IncludeNonPublicTypes = true;
            return base.OnConfigurationCreated(configuration);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Resolve_WithIncludeNonPublicTypesEnable_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<InternalSystemUnderTest>();
            this.Container.Resolve<Mock<IMockable>>()
                .Setup(m => m.Get())
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
        }
    }
}

namespace Tethos.FakeItEasy.Tests.AutoMockingTest.Configuration.IncludeNonPublicTypes
{
    using Castle.MicroKernel;
    using FluentAssertions;
    using Tethos.Tests.Common;
    using Xunit;

    public class DefaultNonPublicTypesTests : FakeItEasy.AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Resolve_WithDefaultIncludeNonPublicTypesConfiguration_ShouldThrowComponentNotFoundException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<InternalSystemUnderTest>();

            // Act & Assert
            sut.Should().Throw<ComponentNotFoundException>();
        }
    }
}

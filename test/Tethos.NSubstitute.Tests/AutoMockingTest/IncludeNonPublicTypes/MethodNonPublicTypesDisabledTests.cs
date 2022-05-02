namespace Tethos.NSubstitute.Tests.AutoMockingTest.Configuration;

using Castle.MicroKernel;
using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

public class MethodNonPublicTypesDisabledTests : NSubstitute.AutoMockingTest
{
    public override AutoMockingConfiguration OnConfigurationCreated(AutoMockingConfiguration configuration)
    {
        configuration.IncludeNonPublicTypes = false;
        return base.OnConfigurationCreated(configuration);
    }

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

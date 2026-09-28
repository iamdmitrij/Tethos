namespace Tethos.NSubstitute.Tests.AutoMockingTest.Configuration;

using AutoFixture.Xunit3;
using global::NSubstitute;
using Shouldly;
using Tethos.Tests.Common;
using Xunit;

public class MethodNonPublicTypesEnabledTests : NSubstitute.AutoMockingTest
{
    public override AutoMockingConfiguration OnConfigurationCreated(AutoMockingConfiguration configuration)
    {
        configuration.IncludeNonPublicTypes = true;
        return base.OnConfigurationCreated(configuration);
    }

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
        actual.ShouldBe(expected);
    }
}

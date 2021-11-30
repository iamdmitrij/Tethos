namespace Tethos.FakeItEasy.Tests.AutoMockingTest
{
    using AutoFixture.Xunit2;
    using Castle.MicroKernel;
    using FluentAssertions;
    using global::FakeItEasy;
    using global::FakeItEasy.Core;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class InternalTests : FakeItEasy.AutoMockingTest
    {
        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Exercise_InternalClass_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<InternalSystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();
            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Exercise_InternalDependency_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTestWithInternal>();
            var mock = this.Container.Resolve<IInternalMockable>();
            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Resolve_LooseInternalDependency_ShouldThrowGeneratorException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<InternalDependency.Tests.SystemUnderTest>();

            // Act & Assert
            sut.Should().Throw<FakeCreationException>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ResolveFrom_LooseInternalDependency_ShouldThrowGeneratorException()
        {
            // Arrange
            var sut = () => this.Container.ResolveFrom<InternalDependency.Tests.SystemUnderTest, InternalDependency.Tests.IMockable>();

            // Act & Assert
            sut.Should().Throw<FakeCreationException>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Exercise_LooseInternalDependency_ShouldThrowComponentNotFoundException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<InternalDependency.Tests.IMockable>();

            // Act & Assert
            sut.Should().Throw<ComponentNotFoundException>();
        }
    }
}

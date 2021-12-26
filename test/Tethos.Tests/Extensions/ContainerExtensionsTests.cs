namespace Tethos.Tests.Extensions
{
    using System;
    using System.Linq;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel;
    using FluentAssertions;
    using Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Attributes;
    using Xunit;

    public class ContainerExtensionsTests
    {
        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void AddDependencyTo_ShouldMatch(Arguments sut, string name, int expected)
        {
            // Act
            var dependency = sut.AddDependencyTo<string, int>(name, expected);
            var actual = dependency[$"{typeof(string)}__{name}"];

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void AddDependencyTo_WithNullValue_ShouldBeNull(Arguments sut, string name)
        {
            // Arrange
            object expected = null;

            // Act
            var dependency = sut.AddDependencyTo<string, object>(name, expected);
            var actual = dependency[$"{typeof(string)}__{name}"];

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void AddDependencyTo_WithNameValue_ShouldThrowArgumentNullException(Arguments sut, int value)
        {
            // Arrange
            string expected = null;

            // Act
            var actual = () => sut.AddDependencyTo<string, int>(expected, value);

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void AddDependencyTo_UsingTypeParam_ShouldMatch(Arguments sut, string name, int expected)
        {
            // Arrange
            var type = typeof(string);

            // Act
            var dependency = sut.AddDependencyTo(type, name, expected);
            var actual = dependency[$"{type}__{name}"];

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void AddDependencyTo_UsingTypeParam_WithNullValue_ShouldBeNull(Arguments sut, string name)
        {
            // Arrange
            object expected = null;
            var type = typeof(string);

            // Act
            var dependency = sut.AddDependencyTo(type, name, expected);
            var actual = dependency[$"{type}__{name}"];

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void AddDependencyTo_UsingTypeParam_WithNameValue_ShouldThrowArgumentNullException(Arguments sut, int value)
        {
            // Arrange
            string expected = null;

            // Act
            var actual = () => sut.AddDependencyTo(typeof(string), expected, value);

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [AutoMoqData]
        [Trait("Type", "Unit")]
        public void ResolveFrom_UsingGenerics_ShouldMatch(int expected, Mock<IAutoMockingContainer> mock)
        {
            // Arrange
            var childType = expected.GetType();
            var parentType = typeof(string);
            mock.Setup(m => m.Resolve(childType)).Returns(expected);

            // Act
            var actual = mock.Object.ResolveFrom<string, int>();

            // Assert
            mock.Verify(m => m.Resolve(parentType), Times.Once);
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Type", "Unit")]
        public void ResolveFrom_UsingTypeParams_ShouldMatch(int expected, Type parent, Mock<IAutoMockingContainer> mock)
        {
            // Arrange
            var childType = expected.GetType();
            mock.Setup(m => m.Resolve(childType)).Returns(expected);

            // Act
            var actual = mock.Object.ResolveFrom(parent, childType);

            // Assert
            mock.Verify(m => m.Resolve(parent), Times.Once);
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Unit")]
        public void Flatten_ShouldMatchArgumentsValueArray(Arguments sut)
        {
            // Arrange
            var expected = sut.Select(argument => argument.Value);

            // Act
            var actual = sut.Flatten();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

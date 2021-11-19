using AutoFixture.Xunit2;
using Castle.MicroKernel;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tethos.Tests
{
    public class ContainerUtilsTests
    {
        [Theory, AutoData]
        [Trait("Category", "Public")]
        public void AddDependencyTo_ShouldMatchArguments(Arguments sut, string name, int expected)
        {
            // Act
            var actual = sut.AddDependencyTo<string, int>(name, expected);

            // Assert
            actual[$"{typeof(string)}__{name}"].Should().Be(expected);
        }

        [Theory, AutoData]
        [Trait("Category", "Public")]
        public void AddDependencyTo_WithNullValue_ShouldBeBull(Arguments sut, string name)
        {
            // Arrange
            object expected = null;

            // Act
            var actual = sut.AddDependencyTo<string, object>(name, expected);

            // Assert
            actual[$"{typeof(string)}__{name}"].Should().Be(expected);
        }

        [Theory, AutoData]
        [Trait("Category", "Public")]
        public void AddDependencyTo_WithNameValue_ShouldThrowArgumentNullException(Arguments sut, int value)
        {
            // Arrange
            string expected = null;

            // Act
            Action actual = () => sut.AddDependencyTo<string, int>(expected, value);

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoData]
        [Trait("Category", "Public")]
        public void AddDependencyTo_UsingTypeParam_ShouldMatchArguments(Arguments sut, string name, int expected)
        {
            // Act
            var actual = sut.AddDependencyTo(typeof(string), name, expected);

            // Assert
            actual[$"{typeof(string)}__{name}"].Should().Be(expected);
        }

        [Theory, AutoData]
        [Trait("Category", "Public")]
        public void AddDependencyTo_UsingTypeParam_WithNullValue_ShouldBeBull(Arguments sut, string name)
        {
            // Arrange
            object expected = null;

            // Act
            var actual = sut.AddDependencyTo(typeof(string), name, expected);

            // Assert
            actual[$"{typeof(string)}__{name}"].Should().Be(expected);
        }

        [Theory, AutoData]
        [Trait("Category", "Public")]
        public void AddDependencyTo_UsingTypeParam_WithNameValue_ShouldThrowArgumentNullException(Arguments sut, int value)
        {
            // Arrange
            string expected = null;

            // Act
            Action actual = () => sut.AddDependencyTo(typeof(string), expected, value);

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoData]
        [Trait("Category", "Internal")]
        public void Flatten_ShouldReturnValueArray(Arguments sut)
        {
            // Arrange
            var expected = sut.Select(x => x.Value);

            // Act
            var actual = sut.Flatten();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

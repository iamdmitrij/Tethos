namespace Tethos.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using FluentAssertions;
using Moq;
using Tethos.Tests.Attributes;
using Tethos.Tests.SUT;
using Xunit;

public class BaseAutoResolverTests
{
    [Theory]
    [InlineData(typeof(IList), true)]
    [InlineData(typeof(IEnumerable), true)]
    [InlineData(typeof(Array), false)]
    [InlineData(typeof(Enumerable), false)]
    [InlineData(typeof(Type), false)]
    [InlineData(typeof(BaseAutoResolver), false)]
    [InlineData(typeof(TimeoutException), false)]
    [InlineData(typeof(Guid), false)]
    [InlineData(typeof(Task<>), false)]
    [InlineData(typeof(Task<int>), false)]
    [InlineData(typeof(int), false)]
    [Trait("Type", "Unit")]
    public void CanResolve_ShouldMatch(
        Type type,
        bool expected)
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var resolver = fixture.Create<CreationContext>();
        var sut = new AutoResolver(Mock.Of<IKernel>());
        var key = "key";

        // Act
        var actual = sut.CanResolve(
            resolver,
            resolver,
            new(),
            new(key, type, false));

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void Resolve_ShouldMatch(
        Mock<IKernel> kernel,
        Mock<object> expected,
        CreationContext resolver,
        string key)
    {
        // Arrange
        var type = expected.GetType();
        kernel.Setup(mock => mock.Resolve(type)).Returns(expected);
        var sut = new AutoResolver(kernel.Object);

        // Act
        var actual = sut.Resolve(
            resolver,
            resolver,
            new(),
            new(key, type, false)) as MockMapping;

        // Assert
        actual.TargetType.Should().Be(type);
        actual.TargetObject.Should().Be(expected);
        actual.ConstructorArguments.Should().BeEmpty();
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void Resolve_Arguments_ShouldMatch(
        Mock<IKernel> kernel,
        Mock<object> expected,
        CreationContext resolver,
        string key)
    {
        // Arrange
        var type = expected.GetType();
        kernel.Setup(mock => mock.Resolve(type)).Returns(expected);
        var sut = new AutoResolver(kernel.Object);

        resolver.AdditionalArguments.Add(new Arguments().AddNamed($"{type}__name", key));

        // Act
        var actual = sut.Resolve(
            resolver,
            resolver,
            new(),
            new(key, type, false)) as MockMapping;

        // Assert
        actual.TargetType.Should().Be(type);
        actual.TargetObject.Should().Be(expected);
        actual.ConstructorArguments.Should().HaveSameCount(resolver.AdditionalArguments);
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void Resolve_NonMatchingArguments_ShouldMatch(
        Mock<IKernel> kernel,
        Mock<object> expected,
        CreationContext resolver,
        string key,
        IList<string> arguments)
    {
        // Arrange
        var type = expected.GetType();
        kernel.Setup(mock => mock.Resolve(type)).Returns(expected);
        var sut = new AutoResolver(kernel.Object);

        resolver.AdditionalArguments.Add(new Arguments().AddNamed($"{arguments.GetType()}__name", key));

        // Act
        var actual = sut.Resolve(
            resolver,
            resolver,
            new(),
            new(key, type, false)) as MockMapping;

        // Assert
        actual.TargetType.Should().Be(type);
        actual.TargetObject.Should().Be(expected);
        actual.ConstructorArguments.Should().BeEmpty();
    }
}

namespace Tethos.Moq.Tests;

using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using global::Moq;
using Tethos.Moq.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

public class AutoResolverTests
{
    [Theory]
    [InlineData(typeof(IList), 1, true)]
    [InlineData(typeof(IEnumerable), 2, true)]
    [InlineData(typeof(Array), 5, true)]
    [InlineData(typeof(Enumerable), 15, true)]
    [InlineData(typeof(Type), 4, true)]
    [InlineData(typeof(BaseAutoResolver), 8, true)]
    [InlineData(typeof(TimeoutException), 10, true)]
    [InlineData(typeof(Guid), 2, false)]
    [InlineData(typeof(Task<>), 4, true)]
    [InlineData(typeof(Task<int>), 10, true)]
    [InlineData(typeof(int), 8, false)]
    [InlineData(typeof(IList), 0, true)]
    [InlineData(typeof(Array), 0, false)]
    [InlineData(typeof(Guid), 0, false)]
    [InlineData(typeof(int), 0, false)]
    [Trait("Type", "Unit")]
    public void CanResolve_ShouldMatch(
        Type type,
        int arguments,
        bool expected)
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var resolver = fixture.Create<CreationContext>();
        var sut = new AutoResolver(Mock.Of<IKernel>());
        var key = "key";
        Enumerable.Range(0, arguments)
            .Select(_ => new Arguments().AddNamed($"{Guid.NewGuid()}", Guid.NewGuid()))
            .ToList()
            .ForEach(argument => resolver.AdditionalArguments.Add(argument));

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
    public void MapToMock_ShouldRegisterMock(Mock<IKernel> kernel, object targetObject, IMockable mockable)
    {
        // Arrange
        var expected = mockable.GetType();
        var sut = new AutoResolver(kernel.Object);
        var type = typeof(IMockable);

        MockMapping argument = new() { TargetType = type, TargetObject = targetObject };

        // Act
        var actual = sut.MapToMock(argument);

        // Assert
        kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.AtLeastOnce);
        actual.Should().BeOfType(expected);
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void MapToMock_ShouldNotRegisterMock(Mock<IKernel> kernel, IMockable mockable)
    {
        // Arrange
        var expected = mockable.GetType();
        var sut = new AutoResolver(kernel.Object);
        var type = typeof(IMockable);

        MockMapping argument = new() { TargetType = type, TargetObject = mockable };

        // Act
        var actual = sut.MapToMock(argument);

        // Assert
        kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.Never);
        actual.Should().BeOfType(expected);
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void MapToMock_WithConstructorArguments_ShouldMatchMockType(Mock<IKernel> kernel, Mock<Concrete> mockable)
    {
        // Arrange
        var expected = mockable.Object.GetType();
        var sut = new AutoResolver(kernel.Object);
        var type = typeof(Concrete);
        var constructorArguments = new Arguments()
            .AddNamed("minValue", 100)
            .AddNamed("maxValue", 200);

        MockMapping argument = new() { TargetType = type, TargetObject = mockable.Object, ConstructorArguments = constructorArguments };

        // Act
        var actual = sut.MapToMock(argument);

        // Assert
        kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.Never);
        actual.Should().BeOfType(expected);
    }
}

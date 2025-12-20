namespace Tethos.FakeItEasy.Tests;

using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using global::FakeItEasy;
using Tethos.FakeItEasy.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

public class AutoResolverTests
{
    [Theory]
    [InlineData(typeof(IList), true)]
    [InlineData(typeof(IEnumerable), true)]
    [InlineData(typeof(Array), true)]
    [InlineData(typeof(Enumerable), true)]
    [InlineData(typeof(Type), true)]
    [InlineData(typeof(BaseAutoResolver), true)]
    [InlineData(typeof(TimeoutException), true)]
    [InlineData(typeof(Guid), false)]
    [InlineData(typeof(Task<>), true)]
    [InlineData(typeof(Task<int>), true)]
    [InlineData(typeof(int), false)]
    [Trait("Type", "Unit")]
    public void CanResolve_ShouldMatch(
        Type type,
        bool expected)
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        var resolver = fixture.Create<CreationContext>();
        var sut = new AutoResolver(fixture.Create<IKernel>());
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
    [AutoFakeItEasyData]
    [Trait("Type", "Unit")]
    public void MapToMock_ShouldRegisterMock(IKernel kernel, object targetObject, IMockable mockable)
    {
        // Arrange
        var expected = mockable.GetType();
        var sut = new AutoResolver(kernel);
        var type = typeof(IMockable);

        MockMapping argument = new() { TargetType = type, TargetObject = targetObject };

        // Act
        var actual = sut.MapToMock(argument);

        // Assert
        A.CallTo(() => kernel.Register(A<IRegistration>._)).MustHaveHappenedOnceExactly();
        actual.Should().BeOfType(expected);
    }

    [Theory]
    [AutoFakeItEasyData]
    [Trait("Type", "Unit")]
    public void MapToMock_ShouldNotRegisterMock(IKernel kernel, IMockable mockable)
    {
        // Arrange
        var expected = mockable.GetType();
        var sut = new AutoResolver(kernel);
        var type = typeof(IMockable);

        MockMapping argument = new() { TargetType = type, TargetObject = mockable };

        // Act
        var actual = sut.MapToMock(argument);

        // Assert
        A.CallTo(() => kernel.Register(A<IRegistration>._)).MustNotHaveHappened();
        actual.Should().BeOfType(expected);
    }

    [Theory]
    [AutoFakeItEasyData]
    [Trait("Type", "Unit")]
    public void MapToMock_WithConstructorArguments_ShouldMatchMockType(IKernel kernel, IMockable mockable)
    {
        // Arrange
        var expected = mockable.GetType();
        var sut = new AutoResolver(kernel);
        var type = typeof(Concrete);
        var constructorArguments = new Arguments()
            .AddNamed("minValue", 100)
            .AddNamed("maxValue", 200);

        MockMapping argument = new() { TargetType = type, TargetObject = mockable, ConstructorArguments = constructorArguments };

        // Act
        var actual = sut.MapToMock(argument);

        // Assert
        A.CallTo(() => kernel.Register(A<IRegistration>._)).MustNotHaveHappened();
        actual.Should().BeOfType(expected);
    }
}

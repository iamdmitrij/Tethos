﻿namespace Tethos.NSubstitute.Tests.AutoMockingTest;

using System;
using AutoFixture.Xunit2;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using global::NSubstitute;
using Tethos.Extensions;
using Tethos.Tests.Common;
using Xunit;

public class AutoMockingTestTests : NSubstitute.AutoMockingTest
{
    [Fact]
    [Trait("Type", "Integration")]
    public void Container_ShouldHaveAutoResolverInstalled()
    {
        // Assert
        this.AutoResolver.Should().BeOfType<AutoResolver>();
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void SystemUnderTest_Exercise_ShouldMatch(int expected)
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderTest>();
        this.Container.Resolve<IMockable>()
            .Get()
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Clean_ShouldRevertBackToOriginalBehavior(Mockable mockable)
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderTest>();
        var action = () => this.Container.Resolve<SystemUnderTest>().Exercise();

        this.Container.Register(Component.For<SystemUnderTest>()
            .OverridesExistingRegistration()
            .DependsOn(Dependency.OnValue<IMockable>(mockable)));

        // Act
        this.Clean();
        sut.Exercise();

        // Assert
        action.Should().Throw<NotImplementedException>();
    }
}

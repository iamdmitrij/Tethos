# Tethos

![Build](https://github.com/iamdmitrij/Tethos/actions/workflows/dotnet.yml/badge.svg?branch=main)
[![Version](https://img.shields.io/nuget/vpre/Tethos.svg)](https://www.nuget.org/packages/Tethos)
[![Downloads](https://img.shields.io/nuget/dt/Tethos.svg)](https://www.nuget.org/packages/Tethos)

`Tethos` is automated auto-mocking system which utilizes `Castle.Windsor` as backbone for working with mocked dependencies used during unit testing. It is test framework agnostic.

### Why?

Consider following example:

```c#
public class SystemUnderTest
{
    public SystemUnderTest(IMockA mockA, IMockB mockB, IMockC mockC, IMockD mockD)
    {
        ...
    }

    public int Do()
    {
        MockA.Do();
        MockB.Do();
        MockC.Do();
        MockD.Do();
    }
}
```

in order to resolve dependencies for `SystemUnderTest` we will need to write following unit test:

```c#
[Fact]
public void Test()
{
    var sut = new SystemUnderTest(
        new MockA(new UnknownA(new UnknownB(), new UnknownC()))
        new MockB(new UnknownC())
        new MockC(new UnknownD())
        new MockD(new UnknownD())
    );

    ...
}
```

with `Tethos.Moq` you will just need to:

```c#
[Fact]
public void Test()
{
    var sut = Container.Resolve<SystemUnderTest>();

    ...
}
```

This saves time to manually injecting mocked dependencies leaving you more time to focus on test themselves.

### Resolving mocks

To resolve mock to unit test simply resolve Mocking type and it will be resolved automatically.

In this example `Moq` is used:

```c#
[Fact]
public void Do()
{
    // Arrange
    var mock = Container.Resolve<Mock<SystemUnderTest>>();

    mock.Setup(x => x.Do())
        .Returns(expected);
    ...
}

```

within the scope of the test method dependencies, including mock instances will be exactly the same.

## Usage

- Inherit from `AutoMockingTest` to have access to `Container` property.

```c#
public class ContainerFromBaseClass: AutoMockingTest
{
    [Fact]
    public void Do_ShouldReturn42()
    {
        var sut = Container.Resolve<SystemUnderTest>();
        ...
    }
}
```

- Inject or initialize `IAutoMoqContainer` dependency using factory method: `AutoMoqContainerFactory.Create()`.

```c#
public class ContainerAsProperty: AutoMockingTest
{
    public IAutoMoqContainer Container { get; }

    public ContainerAsProperty()
    {
        Container = AutoMoqContainerFactory.Create();
    }

    [Fact]
    public void Do_ShouldReturn42()
    {
        var sut = Container.Resolve<SystemUnderTest>();
        ...
    }
}
```

## How it works?

`Tethos` scans project's output assemblies, registers them to IoC container and install mocking interceptor.

### Assembly scanning pattern

Assemblies are selected according to prefix in the name. I.e, if you test assembly is named Project.Tests, then `Tethos` will load every single `Project.*` assembly into auto-mocking container.

### Auto-mocking pattern

Every single incoming dependency will be mocked if such dependency is an interface.

For example this dependency is will resolved without any issues.

```c#
public SystemUnderTest(IDependency dependency, ISubDependency subDependency)
{
    ...
}
```

while here

```c#
public SystemUnderTest(int dependency, string subDependency)
{
    ...
}
```

here, you will need to tell container what to resolve.

```c#
var sut = Container.Resolve<SystemUnderTest>(
    new Arguments()
        .AddTyped(42)
        .AddTyped("foo")
);
```

### Tethos.Moq

Tethos uses Moq to auto-mock incoming dependencies.

# Tethos

![Build](https://github.com/iamdmitrij/Tethos/actions/workflows/nuget.yml/badge.svg?branch=main)

`Tethos` is automated auto-mocking system which utilizes `Castle.Windsor` as backbone for working with mocked dependencies used during unit testing. It is test framework agnostic. `Tethos` supports all popular mocking libraries - `Moq`, `NSubstitute` and `FakeItEasy`.

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
        new Mock<MockA>(new Mock<UnknownA>(new Mock<UnknownB>().Object, new Mock<UnknownC>().Object).Object).Object,
        new Mock<MockB>(new Mock<UnknownC>().Object).Object,
        new Mock<MockC>(new Mock<UnknownD>().Object, new Mock<UnknownE>().Object).Object,
        new Mock<MockD>(new Mock<UnknownD>().Object).Object
    );

    ...
}
```

with `Tethos` all you need to do is:

```c#
[Fact]
public void Test()
{
    var sut = Container.Resolve<SystemUnderTest>();
    var mockA = Container.Resolve<Mock<MockA>>().Setup(...);
    var mockB = Container.Resolve<Mock<MockB>>().Setup(...);
    ...
}
```

This saves time to manually injecting mocked dependencies leaving you more time to focus on test themselves. Tests like this also become easily maintainable. Furthermore, `Tethos` will make sure to scan you test project references to load proper assemblies into the container.

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

### Mocking implementations

#### Tethos.Moq

[![Version](https://img.shields.io/nuget/vpre/Tethos.Moq.svg)](https://www.nuget.org/packages/Tethos.Moq)
[![Downloads](https://img.shields.io/nuget/dt/Tethos.Moq.svg)](https://www.nuget.org/packages/Tethos.Moq)

Tethos uses [Moq](https://www.moqthis.com/moq4/) to auto-mock incoming dependencies. Mocks can resolved using `Mock<>` wrapper type.

```c#
[Fact]
public void Test()
{
    var mock = Container.Resolve<Mock<IMockable>>().Setup(...);
}
```

#### Tethos.NSubstitute

[![Version](https://img.shields.io/nuget/vpre/Tethos.NSubstitute.svg)](https://www.nuget.org/packages/Tethos.NSubstitute)
[![Downloads](https://img.shields.io/nuget/dt/Tethos.NSubstitute.svg)](https://www.nuget.org/packages/Tethos.NSubstitute)

Tethos uses [NSubstitute](https://nsubstitute.github.io/) to auto-mock incoming dependencies. Since `NSubstitute` alter object direct, mock can resolve using direct types.

```c#
[Fact]
public void Test()
{
    var mock = Container.Resolve<IMockable>(); // <-- This will be mocked
}
```

#### Tethos.FakeItEasy

[![Version](https://img.shields.io/nuget/vpre/Tethos.FakeItEasy.svg)](https://www.nuget.org/packages/Tethos.FakeItEasy)
[![Downloads](https://img.shields.io/nuget/dt/Tethos.FakeItEasy.svg)](https://www.nuget.org/packages/Tethos.FakeItEasy)

Tethos uses [FakeItEasy](https://fakeiteasy.github.io/) to auto-mock incoming dependencies. `FakeItEasy` also doesn't wrap mocked object, same here:

```c#
[Fact]
public void Test()
{
    var mock = Container.Resolve<IMockable>(); // <-- This will be mocked
}
```

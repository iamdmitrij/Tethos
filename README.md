# Tethos

![CI](https://img.shields.io/github/workflow/status/iamdmitrij/Tethos/ci?style=flat&logo=github)
[![codecov](https://codecov.io/gh/iamdmitrij/Tethos/branch/main/graph/badge.svg?token=F4IE0T79QP)](https://codecov.io/gh/iamdmitrij/Tethos)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=iamdmitrij_Tethos&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=iamdmitrij_Tethos)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=iamdmitrij_Tethos&metric=ncloc)](https://sonarcloud.io/dashboard?id=iamdmitrij_Tethos)


`Tethos` is automated auto-mocking system which utilizes `Castle.Windsor` as backbone for working with mocked dependencies used during unit testing. It is test framework agnostic. `Tethos` supports all popular mocking libraries - `Moq`, `NSubstitute` and `FakeItEasy`:

| Package            | NuGet                                                                                                                                                                                                                                               |
| ------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Tethos.Moq         | [![Version](https://img.shields.io/nuget/vpre/Tethos.Moq.svg)](https://www.nuget.org/packages/Tethos.Moq) [![Downloads](https://img.shields.io/nuget/dt/Tethos.Moq.svg)](https://www.nuget.org/packages/Tethos.Moq)                                 |
| Tethos.FakeItEasy  | [![Version](https://img.shields.io/nuget/vpre/Tethos.FakeItEasy.svg)](https://www.nuget.org/packages/Tethos.FakeItEasy) [![Downloads](https://img.shields.io/nuget/dt/Tethos.FakeItEasy.svg)](https://www.nuget.org/packages/Tethos.FakeItEasy)     |
| Tethos.NSubstitute | [![Version](https://img.shields.io/nuget/vpre/Tethos.NSubstitute.svg)](https://www.nuget.org/packages/Tethos.NSubstitute) [![Downloads](https://img.shields.io/nuget/dt/Tethos.NSubstitute.svg)](https://www.nuget.org/packages/Tethos.NSubstitute) |

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
        Mock.Of<MockA>(Mock.Of<UnknownA>(Mock.Of<UnknownB>(), Mock.Of<UnknownC>())),
        Mock.Of<MockB>(Mock.Of<UnknownC>()),
        Mock.Of<MockC>(Mock.Of<UnknownD>(), Mock.Of<UnknownE>()),
        Mock.Of<MockD>(Mock.Of<UnknownD>())
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

    mock.Setup(m => m.Do())
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

- Inject or initialize `IAutoMoqContainer` dependency using factory method: `AutoMockingContainerFactory.Create()`.

```c#
public class ContainerAsProperty: AutoMockingTest
{
    public IAutoMoqContainer Container { get; }

    public ContainerAsProperty()
    {
        Container = AutoMockingContainerFactory.Create();
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
    var mock = Container.Resolve<Mock<IMockable>>();
    mock.Setup(m => m.Do()).Returns(42);
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
    mock.Do().Returns(42);
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
    A.CallTo(() => mock.Do()).Returns(42)
}
```

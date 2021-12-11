# Tethos

![CI](https://img.shields.io/github/workflow/status/iamdmitrij/Tethos/ci?style=flat&logo=github)
[![Coverage](https://codecov.io/gh/iamdmitrij/Tethos/branch/main/graph/badge.svg?token=F4IE0T79QP)](https://codecov.io/gh/iamdmitrij/Tethos)
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

    public int Exercise()
    {
        MockA.Get();
        MockB.Get();
        MockC.Get();
        MockD.Get();
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
public void Test_Exercise()
{
    // Arrange
    var mock = Container.Resolve<Mock<IMockable>>();

    mock.Setup(m => m.Get())
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
    public void Exercise_ShouldReturn42()
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
    public void Exercise_ShouldReturn42()
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

Else, `Tethos` will load project referenced assemblies into container.

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

in case there multiple dependencies with same or different types

```c#
public SystemUnderTest(int minValue, int maxValue)
{
    ...
}
```

You can use `AddDependencyTo` extension method to add dependency to certain parameter in the constructor.

```c#
var sut = this.Container.Resolve<SystemUnderTest>(
    new Arguments()
        .AddDependencyTo<Concrete, int>(nameof(minValue), minValue)
        .AddDependencyTo<Concrete, int>(nameof(maxValue), maxValue));
);
```

### ResolveFrom functionality

There are use cases when you can need to resolve parent dependency first before you can get to auto-mocked dependency. For this case, `Tethos` has `ResolveFrom<TParent, TChild>` extension method which will basically resolve mocked dependency in one go.

```c#
// Arrange
var mock = this.Container.ResolveFrom<SystemUnderTest, IMockable>();
```

vs.

```c#
// Arrange
_ = this.Container.Resolve<SystemUnderTest>();
var mock = this.Container.Resolve<IMockable>();
```

### Demo

You can find demo projects code in `/demo` folder. There are examples using `Tethos` libraries with:

- xUnit
- NUnit
- MSTest

testing frameworks.

### Working with internal types

Internal dependencies can loaded into auto-mocking container. But due to possible performance caveats it's disabled by default. Check out Configuration section to figure our how to enable it.

### Configuration

`Tethos` can behavior be configured using `AutoMockingConfiguration` class instance.

| Item                  | Description                                                     | Default value |
| --------------------- | --------------------------------------------------------------- | ------------- |
| IncludeNonPublicTypes | Enables internal types to be loaded into auto-mocking container | `False`       |

Since `AutoMockingConfiguration` is virtual you can override in the child class:

```c#
public class Test : AutoMockingTest
{
    public override AutoMockingConfiguration AutoMockingConfiguration => new() { IncludeNonPublicTypes = false };
}
```

alternatively, you can override `OnConfigurationCreated` method which allow you can edit configuration instance directly.

```c#
public class Test : AutoMockingTest
{
    public override AutoMockingConfiguration OnConfigurationCreated(AutoMockingConfiguration configuration)
    {
        configuration.IncludeNonPublicTypes = true;
        return base.OnConfigurationCreated(configuration);
    }
}
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
    mock.Setup(m => m.Get()).Returns(42);
}
```

alternatively, Moq's proxy objects can be resolved as well

```c#
[Fact]
public void Test()
{
    var mock = Container.Resolve<IMockable>();
    Mock.Get(mock).Setup(m => m.Get()).Returns(42);
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
    mock.Get().Returns(42);
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
    A.CallTo(() => mock.Get()).Returns(42)
}
```

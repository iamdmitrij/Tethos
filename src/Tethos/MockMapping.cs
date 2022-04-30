namespace Tethos;

using System;
using Castle.MicroKernel;

internal class MockMapping
{
    /// <summary>
    /// Target type for object to be converted to destination object.
    /// </summary>
    public Type TargetType { get; set; }

    /// <summary>
    /// Current target object available in container.
    /// </summary>
    public object TargetObject { get; set; }

    /// <summary>
    /// Constructor arguments for non-abstract target type.
    /// </summary>
    public Arguments ConstructorArguments { get; set; }
}

namespace Tethos;

/// <summary>
/// Configuration model used by <see cref="AutoMockingContainer"/> system.
/// </summary>
public class AutoMockingConfiguration
{
    /// <summary>
    /// Toggle to include non-public types into auto-mocking container.
    /// </summary>
    public bool IncludeNonPublicTypes { get; set; }
}

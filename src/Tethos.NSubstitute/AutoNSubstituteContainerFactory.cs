namespace Tethos.NSubstitute
{
    /// <summary>
    /// Factory for generating <see cref="IAutoNSubstituteContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoNSubstituteContainerFactory
    {
        /// <summary>
        /// Creates ready to use auto-mocking container.
        /// </summary>
        /// <returns>Auto-mocking container.</returns>
        public static IAutoNSubstituteContainer Create() => new AutoMockingTest().Container;
    }
}

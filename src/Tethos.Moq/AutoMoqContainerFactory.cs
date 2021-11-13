namespace Tethos.Moq
{
    /// <summary>
    /// Factory for generating <see cref="IAutoMoqContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoMoqContainerFactory
    {
        /// <summary>
        /// Creates ready to use auto-mocking container.
        /// </summary>
        public static IAutoMoqContainer Create() => new AutoMockingTest().Container;
    }
}

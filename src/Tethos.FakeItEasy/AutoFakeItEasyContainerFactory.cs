namespace Tethos.FakeItEasy
{
    /// <summary>
    /// Factory for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoFakeItEasyContainerFactory
    {
        /// <summary>
        /// Creates ready to use auto-mocking container.
        /// </summary>
        /// <returns>Auto-mocking container.</returns>
        public static IAutoMockingContainer Create() => new AutoMockingTest().Container;
    }
}

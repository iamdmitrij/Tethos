namespace Tethos.Moq
{
    /// <summary>
    /// Factory for generating <see cref="IAutoMoqContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoMoqContainerFactory
    {
        /// <summary>
        /// Creates ready to use auto-mocking ready.
        /// </summary>
        public static IAutoMoqContainer Create()
        {
            return new AutoMockingTest().Container;
        }
    }
}

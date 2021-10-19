namespace Tethos.NSubstitute
{
    /// <summary>
    /// Factory for generating <see cref="IAutoNSubstituteContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoNSubstituteContainerFactory
    {
        /// <summary>
        /// Creates ready to use auto-mocking ready.
        /// </summary>
        public static IAutoNSubstituteContainer Create()
        {
            return new AutoMockingTest().Container;
        }
    }
}

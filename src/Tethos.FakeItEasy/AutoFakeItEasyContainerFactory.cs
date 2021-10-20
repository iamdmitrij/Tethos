namespace Tethos.FakeItEasy
{
    /// <summary>
    /// Factory for generating <see cref="IAutoFakeItEasyContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoFakeItEasyContainerFactory
    {
        /// <summary>
        /// Creates ready to use auto-mocking ready.
        /// </summary>
        public static IAutoFakeItEasyContainer Create()
        {
            return new AutoMockingTest().Container;
        }
    }
}

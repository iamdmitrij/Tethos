namespace Tethos.Moq
{
    using System;

    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoMocking
    {
        private static readonly Lazy<IAutoMockingContainer> Lazy = new(() => new AutoMockingTest().Container);

        /// <summary>
        /// Gets ready to use auto-mocking container.
        /// </summary>
        public static IAutoMockingContainer Container
        {
            get => Lazy.Value;
        }
    }
}

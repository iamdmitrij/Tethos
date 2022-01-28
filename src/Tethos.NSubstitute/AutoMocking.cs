namespace Tethos.NSubstitute
{
    using System;

    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public sealed class AutoMocking : IDisposable
    {
        [ThreadStatic]
        private static readonly Lazy<IAutoMockingContainer> Lazy =
            new(() => new AutoMockingTest().Container, true);

        private AutoMocking()
        {
        }

        /// <summary>
        /// Gets ready to use auto-mocking container.
        /// </summary>
        public static IAutoMockingContainer Container
        {
            get { return Lazy.Value; }
        }

        public void Dispose()
        {
            Container?.Dispose();
        }
    }
}

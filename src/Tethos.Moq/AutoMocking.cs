namespace Tethos.Moq
{
    using System;

    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public sealed class AutoMocking : IDisposable
    {
        private static readonly Lazy<IAutoMockingContainer> Lazy =
            new(() => new AutoMockingTest().Container, System.Threading.LazyThreadSafetyMode.PublicationOnly);

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

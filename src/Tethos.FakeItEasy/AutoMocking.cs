namespace Tethos.FakeItEasy
{
    using System;

    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoMocking
    {
        [ThreadStatic]
        private static volatile Lazy<IAutoMockingContainer> container;

        /// <summary>
        /// Gets ready to use auto-mocking container.
        /// </summary>
        public static IAutoMockingContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = new(() => new AutoMockingTest().Container);
                }

                return container.Value;
            }
        }
    }
}

namespace Tethos.Moq
{
    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public static class AutoMocking
    {
        private static readonly object Lock = new object();
        private static IAutoMockingContainer @object = null;

        /// <summary>
        /// Gets ready to use auto-mocking container.
        /// </summary>
        public static IAutoMockingContainer Container
        {
            get
            {
                lock (Lock)
                {
                    if (@object == null)
                    {
                        @object = new AutoMockingTest().Container;
                    }

                    return @object;
                }
            }
        }
    }
}

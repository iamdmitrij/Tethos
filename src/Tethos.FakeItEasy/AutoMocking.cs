namespace Tethos.FakeItEasy
{
    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public class AutoMocking
    {
        private static readonly IAutoMockingContainer ContainerValue = new AutoMockingTest().Container;

        private AutoMocking()
        {
        }

        /// <summary>
        /// Gets ready to use auto-mocking container.
        /// </summary>
        public static IAutoMockingContainer Container
        {
            get
            {
                return ContainerValue;
            }
        }
    }
}

namespace Tethos.FakeItEasy
{
    using System;

    /// <summary>
    /// Static entry-point for generating <see cref="IAutoMockingContainer"/> containers used for auto-mocking.
    /// </summary>
    public class AutoMocking
    {
        [ThreadStatic]
        private static volatile Lazy<IAutoMockingContainer> instance;
        private static volatile int instanceCount = 0;
        private bool alreadyDisposed = false;

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
                if (instance == null)
                {
                    instance = new Lazy<IAutoMockingContainer>(() => new AutoMockingTest().Container);
                }

                instanceCount++;
                return instance.Value;
            }
        }

        //public void Dispose()
        //{
        //    if (--instanceCount == 0)
        //    {
        //        this.Dispose(true);
        //        GC.SuppressFinalize(this);
        //    }
        //}

        //// Protected implementation of Dispose pattern.
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (this.alreadyDisposed)
        //    {
        //        return;
        //    }

        //    if (disposing)
        //    {
        //        instance.Value.Dispose();
        //        instance = null;
        //    }

        //    // Free any unmanaged objects here.
        //    this.alreadyDisposed = true;
        //}
    }
}

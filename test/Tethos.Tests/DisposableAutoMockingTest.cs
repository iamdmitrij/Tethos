namespace Tethos.Tests
{
    internal class DisposableAutoMockingTest : BaseAutoMockingTest<AutoMockingContainer>
    {
        internal bool Disposing { get; set; }

        protected override void Dispose(bool disposing) => this.Disposing = disposing;
    }
}

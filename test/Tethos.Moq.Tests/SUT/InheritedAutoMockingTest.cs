namespace Tethos.Moq.Tests.SUT
{
    using global::Moq;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest()
        {
            this.Container = Mock.Of<AutoMockingContainer>();
            this.Proxy = Mock.Get(this.Container);
        }

        public Mock<AutoMockingContainer> Proxy { get; }
    }
}

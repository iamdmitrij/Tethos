namespace Tethos.Moq.Tests.SUT
{
    using global::Moq;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest()
        {
            this.Container = Mock.Of<AutoMoqContainer>();
            this.Proxy = Mock.Get(this.Container);
        }

        public Mock<AutoMoqContainer> Proxy { get; }
    }
}

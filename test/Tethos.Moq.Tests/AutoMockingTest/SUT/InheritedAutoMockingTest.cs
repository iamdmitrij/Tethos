namespace Tethos.Moq.Tests.AutoMockingTest.SUT
{
    using global::Moq;

    public class InheritedAutoMockingTest : Moq.AutoMockingTest
    {
        public InheritedAutoMockingTest()
        {
            this.Container = Mock.Of<AutoMoqContainer>();
            this.Proxy = Mock.Get(this.Container);
        }

        public Mock<AutoMoqContainer> Proxy { get; }
    }
}

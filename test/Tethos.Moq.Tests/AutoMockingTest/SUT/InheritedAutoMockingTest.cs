namespace Tethos.Moq.Tests.AutoMockingTest.SUT
{
    using global::Moq;

    public class InheritedAutoMockingTest : Moq.AutoMockingTest
    {
        public InheritedAutoMockingTest() => this.Container = Mock.Of<AutoMockingContainer>();

        public Mock<AutoMockingContainer> Proxy => Mock.Get(this.Container);
    }
}

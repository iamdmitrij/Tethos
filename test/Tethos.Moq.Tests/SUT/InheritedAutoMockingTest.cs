namespace Tethos.Moq.Tests.SUT
{
    using Moq;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public Mock<AutoMoqContainer> Proxy { get; }

        public InheritedAutoMockingTest()
        {
            Proxy = new Mock<AutoMoqContainer>();
            Container = Proxy.Object;
        }
    }
}

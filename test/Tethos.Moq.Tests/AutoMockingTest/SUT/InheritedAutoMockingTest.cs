namespace Tethos.Moq.Tests.AutoMockingTest.SUT;

using global::Moq;

public class InheritedAutoMockingTest : Moq.AutoMockingTest
{
    public InheritedAutoMockingTest()
    {
        this.Container = Mock.Of<AutoMockingContainer>(MockBehavior.Loose);
        this.Proxy = Mock.Get(this.Container);
    }

    public Mock<AutoMockingContainer> Proxy { get; }
}

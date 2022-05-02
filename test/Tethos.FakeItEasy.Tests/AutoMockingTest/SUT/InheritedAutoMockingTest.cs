namespace Tethos.FakeItEasy.Tests.AutoMockingTest.SUT;

using global::FakeItEasy;

public class InheritedAutoMockingTest : FakeItEasy.AutoMockingTest
{
    public InheritedAutoMockingTest() => this.Proxy = this.Container = A.Fake<AutoMockingContainer>();

    public AutoMockingContainer Proxy { get; }
}

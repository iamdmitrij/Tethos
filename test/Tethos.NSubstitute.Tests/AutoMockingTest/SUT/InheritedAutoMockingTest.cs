namespace Tethos.NSubstitute.Tests.AutoMockingTest.SUT;

using global::NSubstitute;

public class InheritedAutoMockingTest : NSubstitute.AutoMockingTest
{
    public InheritedAutoMockingTest() => this.Proxy = this.Container = Substitute.For<AutoMockingContainer>();

    public AutoMockingContainer Proxy { get; }
}

namespace Tethos.NSubstitute.Tests.AutoMockingTest.SUT
{
    using global::NSubstitute;

    public class InheritedAutoMockingTest : NSubstitute.AutoMockingTest
    {
        public InheritedAutoMockingTest() => this.Proxy = this.Container = Substitute.For<AutoNSubstituteContainer>();

        public AutoNSubstituteContainer Proxy { get; }
    }
}

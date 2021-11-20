namespace Tethos.NSubstitute.Tests.SUT
{
    using global::NSubstitute;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest() => this.Proxy = this.Container = Substitute.For<AutoNSubstituteContainer>();

        public AutoNSubstituteContainer Proxy { get; }
    }
}

namespace Tethos.NSubstitute.Tests.SUT
{
    using NSubstitute;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public AutoNSubstituteContainer Proxy { get; }

        public InheritedAutoMockingTest() => this.Proxy = this.Container = Substitute.For<AutoNSubstituteContainer>();
    }
}

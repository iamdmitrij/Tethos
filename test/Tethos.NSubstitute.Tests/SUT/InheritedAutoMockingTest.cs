using NSubstitute;

namespace Tethos.NSubstitute.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest() => this.Proxy = this.Container = Substitute.For<AutoNSubstituteContainer>();

        public AutoNSubstituteContainer Proxy { get; }
    }
}

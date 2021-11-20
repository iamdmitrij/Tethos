using NSubstitute;

namespace Tethos.NSubstitute.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public AutoNSubstituteContainer Proxy { get; }

        public InheritedAutoMockingTest() => this.Proxy = this.Container = Substitute.For<AutoNSubstituteContainer>();
    }
}

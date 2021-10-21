using NSubstitute;

namespace Tethos.NSubstitute.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest() => Container = Substitute.For<AutoNSubstituteContainer>();
    }
}

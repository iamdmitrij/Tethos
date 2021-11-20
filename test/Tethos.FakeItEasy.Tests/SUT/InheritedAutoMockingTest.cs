using FakeItEasy;

namespace Tethos.FakeItEasy.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest() => this.Proxy = this.Container = A.Fake<AutoFakeItEasyContainer>();

        public AutoFakeItEasyContainer Proxy { get; }
    }
}

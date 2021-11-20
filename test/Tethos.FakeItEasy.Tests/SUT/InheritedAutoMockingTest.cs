using FakeItEasy;

namespace Tethos.FakeItEasy.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public AutoFakeItEasyContainer Proxy { get; }

        public InheritedAutoMockingTest() => this.Proxy = this.Container = A.Fake<AutoFakeItEasyContainer>();
    }
}

namespace Tethos.FakeItEasy.Tests.SUT
{
    using FakeItEasy;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public AutoFakeItEasyContainer Proxy { get; }

        public InheritedAutoMockingTest() => this.Proxy = this.Container = A.Fake<AutoFakeItEasyContainer>();
    }
}

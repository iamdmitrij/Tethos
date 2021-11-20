namespace Tethos.FakeItEasy.Tests.SUT
{
    using global::FakeItEasy;

    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest() => this.Proxy = this.Container = A.Fake<AutoFakeItEasyContainer>();

        public AutoFakeItEasyContainer Proxy { get; }
    }
}

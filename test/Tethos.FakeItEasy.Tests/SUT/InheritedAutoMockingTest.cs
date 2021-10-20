using FakeItEasy;

namespace Tethos.FakeItEasy.Tests.SUT
{
    public class InheritedAutoMockingTest : AutoMockingTest
    {
        public InheritedAutoMockingTest() => Container = A.Fake<AutoFakeItEasyContainer>();
    }
}

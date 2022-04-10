namespace Tethos.Benchmarks.NonPublicTypes
{
    public class FakeItEasyAutoMockingTest : FakeItEasy.AutoMockingTest
    {
        public override AutoMockingConfiguration AutoMockingConfiguration
            => new() { IncludeNonPublicTypes = true };
    }
}

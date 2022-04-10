namespace Tethos.Benchmarks.NonPublicTypes
{
    public class NSubstituteAutoMockingTest : NSubstitute.AutoMockingTest
    {
        public override AutoMockingConfiguration AutoMockingConfiguration
            => new() { IncludeNonPublicTypes = true };
    }
}

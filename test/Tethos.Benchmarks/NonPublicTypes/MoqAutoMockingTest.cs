namespace Tethos.Benchmarks.NonPublicTypes
{
    public class MoqAutoMockingTest : Moq.AutoMockingTest
    {
        public override AutoMockingConfiguration AutoMockingConfiguration
            => new() { IncludeNonPublicTypes = true };
    }
}

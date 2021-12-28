namespace Tethos.Tests.SUT
{
    using Castle.MicroKernel;

    internal class AutoResolver : BaseAutoResolver
    {
        public AutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToMock(MockMapping argument) => argument;
    }
}

using Castle.Windsor;

namespace Tethos.NSubstitute
{
    /// <summary>
    /// Auto-mocking contrainer for <see cref="NSubstitute"/> concrete type.
    /// </summary>
    public class AutoNSubstituteContainer : WindsorContainer, IAutoNSubstituteContainer
    {
    }
}

namespace Tethos.NSubstitute
{
    using Castle.Windsor;

    /// <summary>
    /// Auto-mocking contrainer for <see cref="NSubstitute"/> concrete type.
    /// </summary>
    public class AutoNSubstituteContainer : WindsorContainer, IAutoNSubstituteContainer
    {
    }
}

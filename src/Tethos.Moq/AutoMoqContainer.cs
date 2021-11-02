namespace Tethos.Moq
{
    using Castle.Windsor;

    /// <summary>
    /// Auto-mocking contrainer for <see cref="Moq"/> concrete type.
    /// </summary>
    public class AutoMoqContainer: WindsorContainer, IAutoMoqContainer
    {
    }
}

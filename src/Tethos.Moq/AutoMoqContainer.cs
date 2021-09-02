using Castle.Windsor;

namespace Tethos.Moq
{
    /// <summary>
    /// Auto-mocking contrainer for <see cref="Moq"/> concrete type.
    /// </summary>
    public class AutoMoqContainer: WindsorContainer, IAutoMoqContainer
    {
    }
}

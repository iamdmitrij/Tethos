using Castle.Windsor;

namespace Tethos.FakeItEasy
{
    /// <summary>
    /// Auto-mocking contrainer for <see cref="FakeItEasy"/> concrete type.
    /// </summary>
    public class AutoFakeItEasyContainer : WindsorContainer, IAutoFakeItEasyContainer
    {
    }
}

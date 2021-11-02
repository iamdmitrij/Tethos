namespace Tethos.FakeItEasy
{
    using Castle.Windsor;

    /// <summary>
    /// Auto-mocking contrainer for <see cref="FakeItEasy"/> concrete type.
    /// </summary>
    public class AutoFakeItEasyContainer: WindsorContainer, IAutoFakeItEasyContainer
    {
    }
}

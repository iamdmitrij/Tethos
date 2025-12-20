namespace Tethos.FakeItEasy.Tests.Attributes;

using AutoFixture;
using AutoFixture.Xunit3;

internal class AutoMockingContainerDataAttribute : AutoDataAttribute
{
    public AutoMockingContainerDataAttribute()
        : base(
        () =>
        {
            var fixture = new Fixture();
            fixture.Register(AutoMocking.Create);
            return fixture;
        })
    {
    }
}

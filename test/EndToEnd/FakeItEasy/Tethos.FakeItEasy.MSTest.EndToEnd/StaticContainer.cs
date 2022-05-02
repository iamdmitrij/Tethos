namespace Tethos.FakeItEasy.MSTest.EndToEnd;

using global::FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;

[TestClass]
public class StaticContainer
{
    [TestMethod]
    [TestProperty("Type", "E2E")]
    public void Exercise_WithMock_ShouldReturn42()
    {
        // Arrange
        var expected = 42;
        var sut = AutoMocking.Container.Resolve<SystemUnderTest>();
        var mock = AutoMocking.Container.Resolve<IMockable>();

        A.CallTo(() => mock.Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        Assert.AreEqual(actual, expected);
    }
}

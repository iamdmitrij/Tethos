namespace Tethos.Moq.MSTest.EndToEnd;

using global::Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tethos.Moq;
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

        AutoMocking.Container.Resolve<Mock<IMockable>>()
            .Setup(mock => mock.Get())
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        Assert.AreEqual(actual, expected);
    }
}

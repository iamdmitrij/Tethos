namespace Tethos.MSTest.Demo;

using global::Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tethos.Moq;
using Tethos.Tests.Common;

[TestClass]
public class ContainerFromBaseClass : AutoMockingTest
{
    [TestMethod]
    [TestProperty("Type", "Demo")]
    public void Exercise_WithMock_ShouldReturn42()
    {
        // Arrange
        var expected = 42;
        var sut = this.Container.Resolve<SystemUnderTest>();

        this.Container.Resolve<Mock<IMockable>>()
            .Setup(mock => mock.Get())
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        Assert.AreEqual(actual, expected);
    }
}

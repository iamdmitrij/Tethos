namespace Tethos.MSTest.Demo;

using global::FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tethos.FakeItEasy;
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
        var mock = this.Container.Resolve<IMockable>();

        A.CallTo(() => mock.Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        Assert.AreEqual(expected, actual);
    }
}

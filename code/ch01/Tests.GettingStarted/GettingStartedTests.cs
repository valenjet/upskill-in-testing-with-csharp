using Getting.Started;

namespace Tests.Getting.Started;

public class GettingStartedTests
{
    [Test]
    public void IsPassing_WhenCalled_ExpectTrue()
    {
        // Arrange
        var classUnderTest = new GettingStarted();

        // Act
        var actual = classUnderTest.IsPassing();

        // Assert
        Assert.That(actual, Is.True);
    }
}

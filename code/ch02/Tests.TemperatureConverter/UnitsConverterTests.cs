using Temperature.Helpers;

namespace Tests.Temperature.Helpers;

public class UnitsConverterTests
{
    [Test]
    public void Convert_When32f_Expect0c()
    {
        // Arrange
        var classUnderTest = new UnitsConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(32);

        // Assert
        Assert.That(actual, Is.EqualTo(32));
    }
}
using Physics.Temperature;

namespace Tests.Unit.Physics.Temperature;

public class TestScaleConverter
{
    [TestCase(32, 0)]
    [TestCase(212, 100)]
    [TestCase(-40, -40)]
    [TestCase(-4, -20)]
    [TestCase(98.6, 37)]
    [TestCase(105.6, 40.9)]
    [TestCase(0, -17.8)]
    [TestCase(1, -17.2)]
    [TestCase(999.9, 537.7)]
    [TestCase(-130, -90)]
    public void Test_FahrenheitToCelsius_WithValidValue_ExpectProperReturn(
        decimal value, 
        decimal expected)
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(-456.67, "value cannot be less than -130°F")]
    [TestCase(1000, "value cannot be greater than or equal to 1000°F")]
    [TestCase(-130.1, "value cannot be less than -130°F")]
    [TestCase(1000.1, "value cannot be greater than or equal to 1000°F")]
    public void Test_FahrenheitToCelsius_WithInvalidValue_ExpectArgumentException(
        decimal value,
        string expectedMessage)
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        TestDelegate act = () => classUnderTest.FahrenheitToCelsius(value);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
}
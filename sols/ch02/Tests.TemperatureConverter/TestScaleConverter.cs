using Physics.Temperature;

namespace Tests.Unit.Physics.Temperature;

public class TestScaleConverter
{
    [Test]
    public void Test_FahrenheitToCelsius_When32f_Expect0c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(32);

        // Assert
        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void Test_FahrenheitToCelsius_When212f_Expect100c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(212);

        // Assert
        Assert.That(actual, Is.EqualTo(100));
    }

    [Test]
    public void Test_FahrenheitToCelsius_WhenMinus40f_ExpectMinus40c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(-40);

        // Assert
        Assert.That(actual, Is.EqualTo(-40));
    }

    [Test]
    public void Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(-4);

        // Assert
        Assert.That(actual, Is.EqualTo(-20));
    }

    [Test]
    public void Test_FahrenheitToCelsius_When98pt6f_Expect37c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(98.6m);

        // Assert
        Assert.That(actual, Is.EqualTo(37));
    }

    [Test]
    public void Test_FahrenheitToCelsius_When105pt6f_Expect40pt9c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(105.6m);

        // Assert
        Assert.That(actual, Is.EqualTo(40.9m));
    }
}
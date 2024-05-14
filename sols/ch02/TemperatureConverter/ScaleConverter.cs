namespace Physics.Temperature;

public class ScaleConverter
{
    public decimal FahrenheitToCelsius(decimal value, int precision=1) {
        if(value < -130) {
            throw new ArgumentException("value cannot be less than -130°F");
        }
        if(value >= 1000) {
            throw new ArgumentException("value cannot be greater than or equal to 1000°F");
        }
        return Math.Round((5 * (value -32)) / 9, 1);
    }
}

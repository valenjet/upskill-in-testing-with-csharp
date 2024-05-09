namespace Physics.Temperature;

public class ScaleConverter
{
    public decimal FahrenheitToCelsius(decimal value){
        return Math.Round((5 * (value -32)) / 9, 1);
    }
}

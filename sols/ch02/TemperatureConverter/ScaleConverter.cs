namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        return (5 * (value -32)) / 9;
    }
}

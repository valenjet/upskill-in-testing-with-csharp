namespace Valen.Slos.Dal.Helper;

internal class SqlParameter
{
    public SqlParameter(string parameterName, int integerValue)
    {
        ParameterName = parameterName;
        IntegerValue = integerValue;
    }

    public SqlParameter(string parameterName, decimal decimalValue)
    {
        ParameterName = parameterName;
        DecimalValue = decimalValue;
    }

    public SqlParameter(string parameterName, string? stringValue)
    {
        ParameterName = parameterName;
        StringValue = stringValue;
    }

    public SqlParameter(string parameterName, DateTime? dateTimeValue)
    {
        ParameterName = parameterName;
        DateTimeValue = dateTimeValue;
    }

    public string ParameterName { get; }
    public int? IntegerValue { get; }
    public decimal? DecimalValue { get; }
    public string? StringValue { get; }
    public DateTime? DateTimeValue { get; }
}
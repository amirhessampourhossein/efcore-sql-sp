namespace EntityFrameworkCore.SqlServer.StoredProcedure.Models;

public class Parameter
{
    public string Name { get; set; } = null!;
    public ParameterType Type { get; set; }
    public string Value { get; set; } = null!;
    public string SqlFormattedValue => Type switch
    {
        ParameterType.Numeric => Value,
        ParameterType.String => $"'{Value}'",
        ParameterType.UnicodeString => $"N'{Value}'",
        _ => throw new InvalidOperationException(),
    };
}

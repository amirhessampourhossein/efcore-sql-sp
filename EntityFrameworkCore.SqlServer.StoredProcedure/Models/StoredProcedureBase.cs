namespace EntityFrameworkCore.SqlServer.StoredProcedure.Models;

public abstract class StoredProcedureBase
{
    public abstract string Name { get; }
    public abstract Parameter[] Parameters { get; }

    public string BuildQuery(params string?[] values)
    {
        for (int i = 0; i < Parameters.Length && i < values.Length; i++)
        {
            Parameters[i].Value = values[i]!;
        }

        var formattedParameters = Parameters
            .Select(p => $"@{p.Name} = {p.SqlFormattedValue}")
            .ToArray();

        var query = $"EXEC {Name} {string.Join(' ', formattedParameters)}";

        return query;
    }
}

using EntityFrameworkCore.SqlServer.StoredProcedure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.SqlServer.StoredProcedure;

public class StoredProcedureExecuter
{
    private readonly DatabaseFacade _database;
    private string _sqlQuery;

    public StoredProcedureExecuter(DatabaseFacade database, string name)
    {
        _database = database;
        _sqlQuery = $"EXEC {name}";
    }

    public StoredProcedureExecuter WithParameter(Parameter parameter)
    {
        _sqlQuery += $" @{parameter.Name} = {parameter.SqlFormattedValue}";
        return this;
    }

    public StoredProcedureExecuter WithParameter(string name, string value, ParameterType type)
    {
        var parameter = new Parameter()
        {
            Name = name,
            Value = value,
            Type = type
        };

        return WithParameter(parameter);
    }

    public IQueryable<TResult> Execute<TResult>()
    {
        var result = _database.SqlQuery<TResult>($"{_sqlQuery}");

        return result;
    }

    public int ExecuteNonQuery()
    {
        var rowsAffected = _database.ExecuteSql($"{_sqlQuery}");

        return rowsAffected;
    }

    public async Task<int> ExecuteNonQueryAsync()
    {
        var rowsAffected = await _database.ExecuteSqlAsync($"{_sqlQuery}");

        return rowsAffected;
    }
}

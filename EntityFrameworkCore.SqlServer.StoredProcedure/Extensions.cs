using EntityFrameworkCore.SqlServer.StoredProcedure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.SqlServer.StoredProcedure;

public static class Extensions
{
    /// <summary>
    /// Creates an instance of <see cref="StoredProcedureExecuter"/> to execute a stored procedure
    /// </summary>
    /// <param name="database">The <see cref="DatabaseFacade"/> of your <see cref="DbContext"/></param>
    /// <param name="name">The name of stored procedure</param>
    /// <returns>An instance of <see cref="StoredProcedureExecuter"/></returns>
    public static StoredProcedureExecuter StoredProcedure(
        this DatabaseFacade database,
        string name)
    {
        var executer = new StoredProcedureExecuter(database, name);

        return executer;
    }

    /// <summary>
    /// Creates an instance of <typeparamref name="TProcedure"/> type and performs execution
    /// </summary>
    /// <typeparam name="TProcedure">The stored procedure type passed as a generic argument</typeparam>
    /// <param name="database">The <see cref="DatabaseFacade"/> of your <see cref="DbContext"/></param>
    /// <param name="values">The values for stored procedure parameters</param>
    /// <returns>The number of rows affected</returns>
    public static int ExecuteNonQueryStoredProcedure<TProcedure>(
        this DatabaseFacade database,
        params string?[] values)
        where TProcedure : StoredProcedureBase
    {
        var procedure = Activator.CreateInstance<TProcedure>();

        var query = procedure.BuildQuery(values);

        var rowsAffected = database.ExecuteSql($"{query}");

        return rowsAffected;
    }

    public static int ExecuteNonQueryStoredProcedure<TProcedure>(
        this DatabaseFacade database,
        TProcedure storedProcedure,
        params string?[] values)
        where TProcedure : StoredProcedureBase
    {
        var query = storedProcedure.BuildQuery(values);

        var rowsAffected = database.ExecuteSql($"{query}");

        return rowsAffected;
    }

    public static async Task<int> ExecuteNonQueryStoredProcedureAsync<TProcedure>(
        this DatabaseFacade database,
        params string?[] values)
        where TProcedure : StoredProcedureBase
    {
        var procedure = Activator.CreateInstance<TProcedure>();

        var query = procedure.BuildQuery(values);

        var rowsAffected = await database.ExecuteSqlAsync($"{query}");

        return rowsAffected;
    }

    public static async Task<int> ExecuteNonQueryStoredProcedureAsync<TProcedure>(
        this DatabaseFacade database,
        TProcedure storedProcedure,
        params string?[] values)
        where TProcedure : StoredProcedureBase
    {
        var query = storedProcedure.BuildQuery(values);

        var rowsAffected = await database.ExecuteSqlAsync($"{query}");

        return rowsAffected;
    }

    public static IQueryable<TResult> Execute<TProcedure, TResult>(
        this DatabaseFacade database,
        params string?[] values)
        where TProcedure : StoredProcedureBase
    {
        var procedure = Activator.CreateInstance<TProcedure>();

        var query = procedure.BuildQuery(values);

        var result = database.SqlQuery<TResult>($"{query}");

        return result;
    }

    public static IQueryable<TResult> Execute<TProcedure, TResult>(
        this DatabaseFacade database,
        TProcedure storedProcedure,
        params string?[] values)
        where TProcedure : StoredProcedureBase
    {
        var query = storedProcedure.BuildQuery(values);

        var result = database.SqlQuery<TResult>($"{query}");

        return result;
    }
}

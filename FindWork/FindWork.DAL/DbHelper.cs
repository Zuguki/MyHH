using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace FindWork.DAL;

public static class DbHelper
{
    private const string ConnectionString = "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=test";

    public static async Task<int> ExecuteScalarAsync(string sql, object model)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<int>(sql, model);
        }
    }

    public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(sql, model);
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FindWork.DAL.Models;
using Npgsql;

namespace FindWork.DAL;

public class DbSessionDAL : IDbSessionDAL
{
    public async Task<int> CreateSession(SessionModel model)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            var sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                        values (@DbSessionId, @SessionData, @Created, @LastAccessed, @UserId)";
            return await connection.ExecuteAsync(sql, model);
        }
    }
    
    public async Task<SessionModel?> GetSession(Guid sessionId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            var sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId 
                        from DbSession
                        where DbSessionId = @sessionId";

            var session = await connection.QueryAsync<SessionModel>(sql, new {sessionId});
            return session.FirstOrDefault();
        }
    }

    public async Task<int> UpdateSession(SessionModel model)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            var sql = @"update DbSession
                        set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                        where DbSessionId = @sessionId";

            return await connection.ExecuteAsync(sql, model);
        }
    }
}
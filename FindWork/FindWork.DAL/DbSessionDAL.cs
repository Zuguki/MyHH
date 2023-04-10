using System;
using System.Linq;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL;

public class DbSessionDAL : IDbSessionDAL
{
    public async Task<int> Create(SessionModel model)
    {
        var sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionId, @SessionData, @Created, @LastAccessed, @UserId)";
        return await DbHelper.ExecuteScalarAsync(sql, model);
    }
    
    public async Task<SessionModel?> Get(Guid sessionId)
    {
        var sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId 
                    from DbSession
                    where DbSessionId = @sessionId";

        var sessions = await DbHelper.QueryAsync<SessionModel>(sql, new {sessionId});
        return sessions.FirstOrDefault();
    }
    
    public async Task Lock(Guid sessionId)
    {
        var sql = @"select DbSessionId
                    from DbSession
                    where DbSessionId = @sessionId
                    for update";
        await DbHelper.QueryAsync<SessionModel>(sql, new {sessionId});
    }

    public async Task<int> Update(SessionModel model)
    {
        var sql = @"update DbSession
                    set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                    where DbSessionId = @sessionId";
        return await DbHelper.ExecuteScalarAsync(sql, model);
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FindWork.DAL;

public class UserTokenDAL : IUserTokenDAL
{
    public async Task<Guid> Create(int userId)
    {
        var tokenId = Guid.NewGuid();
        var sql = @"insert into UserToken (UserTokenId, UserId, Created)
                    values(@tokenId, @userId, @date)";
        await DbHelper.ExecuteScalarAsync(sql, new {tokenId = tokenId, userId = userId, date = DateTime.Now});
        
        return tokenId;
    }

    public async Task<int?> Get(Guid tokenId)
    {
        var sql = @"select UserId 
                    from UserToken
                    where UserTokenId = @tokenId";

        var res = await DbHelper.QueryAsync<int>(sql, new {tokenId = tokenId});

        return res.FirstOrDefault();
    }
}
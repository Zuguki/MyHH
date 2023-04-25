using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL;

public class UserSecurityDAL : IUserSecurityDAL
{
    public async Task AddUserSecurity(UserSecurityModel model)
    {
        var sql = @"insert into UserSecurity (UserId, VerificationCode)
                    values (@UserId, @VerificationCode)";

        await DbHelper.ExecuteAsync(sql, model);
    }

    public async Task<UserSecurityModel> GetUserSecurity(int userid)
    {
        var sql = @"select UserId, VerificationCode 
                    from UserSecurityModel 
                    where userid = @userid";

        return await DbHelper.QueryScalarAsync<UserSecurityModel>(sql, new {userid = userid});
    }
}
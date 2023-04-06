using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FindWork.DAL.Models;
using Npgsql;

namespace FindWork.DAL;

public class AuthDal : IAuthDAL
{
    public async Task<UserModel> GetUser(int id)
    {
        var sql = @"select UserId, Email, Password, Salt, Status
                    from AppUser
                    where UserId = @id";
        var users = await DbHelper.QueryAsync<UserModel>(sql, new {id});
        return users.FirstOrDefault() ?? new UserModel();
    }

    public async Task<UserModel> GetUser(string email)
    {
        var sql = @"select UserId, Email, Password, Salt, Status
                    from AppUser
                    where Email = @email";
        var users = await DbHelper.QueryAsync<UserModel>(sql, new {email});
        return users.FirstOrDefault() ?? new UserModel();
    }

    public async Task<int> CreateUser(UserModel model)
    {
        var sql = @"insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status) returning UserId";
        var userIds = await DbHelper.QueryAsync<int>(sql, model);
        return userIds.First();
    }
}
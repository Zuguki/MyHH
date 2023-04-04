using System.Threading.Tasks;
using Dapper;
using FindWork.DAL.Models;
using Npgsql;

namespace FindWork.DAL;

public class AuthDal : IAuthDAL
{
    public async Task<UserModel> GetUser(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                select UserId, Email, Password, Salt, Status
                from AppUser
                where UserId = @id", new {id = id}) ?? new UserModel();
        }
    }

    public async Task<UserModel> GetUser(string email)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            
            return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                select UserId, Email, Password, Salt, Status
                from AppUser
                where Email = @email", new {email = email}) ?? new UserModel();
        }
    }

    public async Task<int> CreateUser(UserModel model)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();

            var sql = @"insert into AppUser(Email, Password, Salt, Status)
                        values(@Email, @Password, @Salt, @Status) returning UserId";
            return await connection.QuerySingleAsync<int>(sql, model);
        }
    }
}
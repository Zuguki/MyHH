using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL.Profile;

public class ProfileDAL : IProfileDAL
{
    public async Task<IEnumerable<ProfileModel>> Get(int userId)
    {
        var sql = @"select ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                    from Profile
                    where UserId = @id";
        
        return await DbHelper.QueryAsync<ProfileModel>(sql, userId);
    }

    public async Task<int> Add(ProfileModel model)
    {
        var sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage)
                    values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage)
                    returning ProfileId";

        var result = await DbHelper.QueryAsync<int>(sql, model);
        return result.First();
    }

    public async Task Update(ProfileModel model)
    {
        var sql = @"update Profile
                    set ProfileName = @ProfileName,
                        FirstName = @FirstName,
                        LastName = @LastName,
                        ProfileImage = @ProfileImage
                    where ProfileId = @ProfileId";
        await DbHelper.QueryAsync<int>(sql, model);
    }
}
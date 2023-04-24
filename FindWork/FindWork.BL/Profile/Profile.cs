using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;
using FindWork.DAL.Profile;

namespace FindWork.BL.Profile;

public class Profile : IProfile
{
    private readonly IProfileDAL profileDal;

    public Profile(IProfileDAL profileDal)
    {
        this.profileDal = profileDal;
    }

    public async Task<IEnumerable<ProfileModel>> Get(int userId)
    {
        return await profileDal.Get(userId);
    }

    public async Task AddOrUpdate(ProfileModel model)
    {
        if (model.ProfileId is null)
            await profileDal.Add(model);
        else
            await profileDal.Update(model);
    }
}
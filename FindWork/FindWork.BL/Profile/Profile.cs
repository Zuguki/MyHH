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

    public async Task<int> Add(ProfileModel model)
    {
        return await profileDal.Add(model);
    }

    public async Task<IEnumerable<ProfileModel>> Get(int userId)
    {
        return await profileDal.Get(userId);
    }

    public async Task Update(ProfileModel model)
    {
        await profileDal.Update(model);
    }
}
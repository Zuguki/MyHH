using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.BL.Profile;

public interface IProfile
{
    Task<int> Add(ProfileModel model);
    
    Task<IEnumerable<ProfileModel>> Get(int userId);

    Task Update(ProfileModel model);
}
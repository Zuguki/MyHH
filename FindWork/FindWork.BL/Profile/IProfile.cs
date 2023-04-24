using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.BL.Profile;

public interface IProfile
{
    Task<IEnumerable<ProfileModel>> Get(int userId);

    Task AddOrUpdate(ProfileModel model);
}
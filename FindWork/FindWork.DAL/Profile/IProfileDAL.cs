using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL.Profile;

public interface IProfileDAL
{
    Task<IEnumerable<ProfileModel>> Get(int userId);

    Task<int> Add(ProfileModel model);

    Task Update(ProfileModel model);
}
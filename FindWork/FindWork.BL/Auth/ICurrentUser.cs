using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();

    Task<int?> GetUserId();
    Task<IEnumerable<ProfileModel>> GetProfiles();
}
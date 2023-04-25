using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL;

public interface IUserSecurityDAL
{
    Task AddUserSecurity(UserSecurityModel model);

    Task<UserSecurityModel> GetUserSecurity(int userid);
}
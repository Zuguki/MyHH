using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.BL.Auth;

public interface IUserSecurity
{
    Task CreateUserVerification(int userid, string email);

    Task<UserSecurityModel> GetUserSecurity(int userid);
}
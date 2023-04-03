using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.BL.Auth;

public interface IAuthBL
{
    Task<int> CreateUser(UserModel model);

    Task<int> Authenticate(string email, string password, bool rememberMe);
}
using System.Threading.Tasks;

namespace FindWork.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();
}
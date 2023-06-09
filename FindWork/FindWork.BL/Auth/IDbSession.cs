using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.BL.Auth;

public interface IDbSession
{
    Task<SessionModel> GetSession();
    
    Task SetUserId(int userId);
    
    Task<int?> GetUserId();
    
    Task<bool> IsLoggedIn();
    
    Task Lock();

    void ResetSessionCache();
}
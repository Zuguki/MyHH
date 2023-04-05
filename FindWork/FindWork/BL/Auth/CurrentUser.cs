using System.Threading.Tasks;

namespace FindWork.BL.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IDbSession dbSession;

    public CurrentUser(IDbSession dbSession)
    {
        this.dbSession = dbSession;
    }

    public async Task<bool> IsLoggedIn()
    {
        return await dbSession.IsLoggedIn();
    }
}
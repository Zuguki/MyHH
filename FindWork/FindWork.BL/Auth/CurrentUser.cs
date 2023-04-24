using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.BL.General;
using FindWork.DAL;
using FindWork.DAL.Models;
using FindWork.DAL.Profile;

namespace FindWork.BL.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IDbSession dbSession;
    private readonly IWebCookie webCookie;
    private readonly IUserTokenDAL userTokenDal;
    private readonly IProfileDAL profileDal;

    public CurrentUser(IDbSession dbSession, IWebCookie webCookie, IUserTokenDAL userTokenDal, IProfileDAL profileDal)
    {
        this.dbSession = dbSession;
        this.webCookie = webCookie;
        this.userTokenDal = userTokenDal;
        this.profileDal = profileDal;
    }

    public async Task<bool> IsLoggedIn()
    {
        var isLoggedIn = await dbSession.IsLoggedIn();
        if (!isLoggedIn)
        {
            var userId = await GetUserIdByToken();
            if (userId is not null && userId != -1)
            {
                await dbSession.SetUserId((int)userId);
                isLoggedIn = true;
            }
        }

        return isLoggedIn;
    }

    public async Task<int?> GetUserId() =>
        await dbSession.GetUserId();

    public async Task<IEnumerable<ProfileModel>> GetProfiles()
    {
        var userId = await GetUserId();
        if (userId is null)
            throw new Exception("User is not found");

        return await profileDal.Get((int) userId);
    }

    private async Task<int?> GetUserIdByToken()
    {
        var cookie = webCookie.Get(AuthConstants.RememberMeCookieName);
        if (cookie is null)
            return null;

        var userTokenId = Helpers.StringToGuidDef(cookie);
        if (userTokenId is null)
            return null;

        return await userTokenDal.Get((Guid) userTokenId);
    }
}
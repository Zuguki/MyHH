using FindWork.BL.Auth;
using FindWork.BL.General;
using FindWork.DAL;
using Microsoft.AspNetCore.Http;

namespace FindWork.Test.Helpers;

public class BaseTest
{
    protected readonly IAuthDAL authDal = new AuthDAL();
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly IEncrypt encrypt = new Encrypt();
    protected readonly IDbSessionDAL sessionDal = new DbSessionDAL();
    protected readonly IDbSession session;
    protected readonly IWebCookie webCookie = new TestWebCookie();
    protected readonly IUserTokenDAL userTokenDal = new UserTokenDAL();
    protected IAuth auth;
    protected ICurrentUser currentUser;
    
    public BaseTest()
    {
        session = new DbSession(sessionDal, webCookie);
        auth = new Auth(authDal, encrypt, session, userTokenDal, webCookie);
        currentUser = new CurrentUser(session, webCookie, userTokenDal);
    }
}
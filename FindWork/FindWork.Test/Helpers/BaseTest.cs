using FindWork.BL.Auth;
using FindWork.BL.General;
using FindWork.DAL;
using Microsoft.AspNetCore.Http;

namespace FindWork.Test.Helpers;

public class BaseTest
{
    protected readonly IAuthDAL authDal = new AuthDal();
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly IEncrypt encrypt = new Encrypt();
    protected readonly IDbSessionDAL sessionDal = new DbSessionDAL();
    protected readonly IDbSession session;
    protected readonly IWebCookie webCookie = new TestWebCookie();
    protected readonly IUserTokenDAL userTokenDal = new UserTokenDAL();
    protected IAuth Auth;
    
    public BaseTest()
    {
        session = new DbSession(sessionDal, webCookie);
        Auth = new Auth(authDal, encrypt, session, userTokenDal, webCookie);
    }
}
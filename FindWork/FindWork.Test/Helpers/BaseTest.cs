using FindWork.BL.Auth;
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
    protected IAuth Auth;
    
    public BaseTest()
    {
        session = new DbSession(sessionDal, httpContextAccessor);
        Auth = new Auth(authDal, encrypt, session);
    }
}
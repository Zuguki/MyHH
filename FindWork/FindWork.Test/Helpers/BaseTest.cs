using FindWork.BL.Auth;
using FindWork.BL.Email;
using FindWork.BL.General;
using FindWork.DAL;
using FindWork.DAL.Email;
using FindWork.DAL.Profile;
using Microsoft.AspNetCore.Http;

namespace FindWork.Test.Helpers;

public class BaseTest
{
    protected readonly IAuthDAL authDal = new AuthDAL();
    protected readonly IDbSessionDAL sessionDal = new DbSessionDAL();
    protected readonly IDbSession session;
    protected readonly IWebCookie webCookie = new TestWebCookie();
    protected readonly IUserTokenDAL userTokenDal = new UserTokenDAL();
    protected readonly IProfileDAL profileDal = new ProfileDAL();
    protected readonly IUserSecurityDAL userSecurityDal = new UserSecurityDAL();
    protected readonly IEmailQueueDAL emailQueueDal = new EmailQueueDAL();
    protected IAuth auth;
    protected ICurrentUser currentUser;
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly IEncrypt encrypt = new Encrypt();
    protected readonly IUserSecurity userSecurity;
    protected readonly IEmailQueue emailQueue;
    
    public BaseTest()
    {
        session = new DbSession(sessionDal, webCookie);
        emailQueue = new EmailQueue(emailQueueDal);
        userSecurity = new UserSecurity(userSecurityDal, emailQueue);
        auth = new Auth(authDal, encrypt, session, userTokenDal, webCookie, userSecurity);
        currentUser = new CurrentUser(session, webCookie, userTokenDal, profileDal);
    }
}
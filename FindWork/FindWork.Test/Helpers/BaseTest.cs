using FindWork.BL.Auth;
using FindWork.DAL;
using Microsoft.AspNetCore.Http;

namespace FindWork.Test.Helpers;

public class BaseTest
{
    protected readonly IAuthDAL authDal = new AuthDal();
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly IEncrypt encrypt = new Encrypt();
    protected IAuthBL authBl;
    
    public BaseTest()
    {
        authBl = new AuthBL(authDal, encrypt, httpContextAccessor);
    }
}
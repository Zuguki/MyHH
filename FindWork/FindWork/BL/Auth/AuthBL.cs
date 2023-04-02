using System;
using System.Threading.Tasks;
using FindWork.DAL;
using FindWork.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace FindWork.BL.Auth;

public class AuthBL : IAuthBL
{
    private readonly IAuthDAL authDal;
    private readonly IEncrypt encrypt;
    private readonly IHttpContextAccessor httpContext;
    
    public AuthBL(IAuthDAL authDal, IEncrypt encrypt, IHttpContextAccessor httpContext)
    {
        this.authDal = authDal;
        this.encrypt = encrypt;
        this.httpContext = httpContext;
    }

    public async Task<int> CreateUser(UserModel model)
    {
        var salt = Guid.NewGuid().ToByteArray();
        model.Password = encrypt.HashPassword(model.Password, salt);
        
        var id = await authDal.CreateUser(model);
        LogIn(id);
        return id;
    }

    public void LogIn(int id)
    {
        httpContext.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
    }
}
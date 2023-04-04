using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FindWork.BL.Exceptions;
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
        var salt = Guid.NewGuid().ToString();
        model.Password = encrypt.HashPassword(model.Password, salt);
        model.Salt = salt;
        
        var id = await authDal.CreateUser(model);
        LogIn(id);
        return id;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await authDal.GetUser(email);
        if (user.UserId is not null && user.Password == encrypt.HashPassword(password, user.Salt))
        {
            LogIn(user.UserId ?? 0);
            return user.UserId ?? 0;
        }

        throw new AuthorizeException();
    }

    public async Task<ValidationResult> ValidateEmail(string email)
    {
        var user = await authDal.GetUser(email);
        return user.UserId is not null ? new ValidationResult("Email already exists") : null;
    }

    public void LogIn(int id)
    {
        httpContext.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
    }
}
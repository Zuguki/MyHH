using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FindWork.BL.Exceptions;
using FindWork.BL.General;
using FindWork.DAL;
using FindWork.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace FindWork.BL.Auth;

public class Auth : IAuth
{
    private readonly IAuthDAL authDal;
    private readonly IEncrypt encrypt;
    private readonly IDbSession dbSession;

    public Auth(IAuthDAL authDal, IEncrypt encrypt, IDbSession dbSession)
    {
        this.authDal = authDal;
        this.encrypt = encrypt;
        this.dbSession = dbSession;
    }

    public async Task<int> CreateUser(UserModel model)
    {
        var salt = Guid.NewGuid().ToString();
        model.Password = encrypt.HashPassword(model.Password, salt);
        model.Salt = salt;
        
        var id = await authDal.CreateUser(model);
        await LogIn(id);
        return id;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await authDal.GetUser(email);
        if (user.UserId is null || user.Password != encrypt.HashPassword(password, user.Salt))
            throw new AuthorizeException();
        
        await LogIn(user.UserId ?? 0);
        return user.UserId ?? 0;
    }

    public async Task ValidateEmail(string email)
    {
        var user = await authDal.GetUser(email);
        if (user.UserId is not null)
            throw new DuplicateEmailException();
    }
    
    public async Task Register(UserModel model)
    {
        using (var scope = Helpers.CreateTransactionScope(600))
        {
            await dbSession.Lock();
            await ValidateEmail(model.Email);
            await CreateUser(model);
            scope.Complete();
        }
    }

    public async Task LogIn(int id)
    {
        await dbSession.SetUserId(id);
    }
}
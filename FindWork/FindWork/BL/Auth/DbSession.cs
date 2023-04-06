using System;
using System.Linq;
using System.Threading.Tasks;
using FindWork.DAL;
using FindWork.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace FindWork.BL.Auth;

public class DbSession : IDbSession
{
    private readonly IDbSessionDAL sessionDal;
    private readonly IHttpContextAccessor httpContextAccessor;
    private SessionModel? sessionModel = null;

    public DbSession(IDbSessionDAL sessionDal, IHttpContextAccessor httpContextAccessor)
    {
        this.sessionDal = sessionDal;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<SessionModel> GetSession()
    {
        if (sessionModel is not null)
            return sessionModel;

        SessionModel? session = null;
        var cookie = httpContextAccessor.HttpContext?.Request.Cookies.FirstOrDefault();
        if (cookie is not null && !string.IsNullOrEmpty(cookie.Value.Value))
        { 
            var sessionId = Guid.Parse(cookie.Value.Value);
            session = await sessionDal.Get(sessionId);
        }

        if (session is null)
        {
            session = await CreateSession();
            CreateSessionCookie(session.DbSessionId);
        }

        sessionModel = session;
        return session;
    }

    public async Task<int> SetUserId(int userId)
    {
        var session = await GetSession();
        session.DbSessionId = Guid.NewGuid();
        session.UserId = userId;
        
        CreateSessionCookie(session.DbSessionId);
        return await sessionDal.Create(session);
    }

    public async Task<int?> GetUserId()
    {
        var session = await GetSession();
        return session.UserId;
    }

    public async Task<bool> IsLoggedIn()
    {
        var session = await GetSession();
        return session.UserId is not null;
    }

    public async Task Lock()
    {
        var data = await GetSession();
        await sessionDal.Lock(data.DbSessionId);
    }

    private async Task<SessionModel> CreateSession()
    {
        var session = new SessionModel()
        {
            DbSessionId = Guid.NewGuid(),
            Created = DateTime.Now,
            LastAccessed = DateTime.Now,
        };

        await sessionDal.Create(session);
        return session;
    }
    
    private void CreateSessionCookie(Guid sessionId)
    {
        var option = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            Secure = true
        };
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(AuthConstants.SessionCookieName);
        httpContextAccessor.HttpContext?.Response.Cookies.Append(AuthConstants.SessionCookieName, sessionId.ToString(), option);
    }
}
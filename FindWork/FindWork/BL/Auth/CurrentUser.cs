using Microsoft.AspNetCore.Http;

namespace FindWork.BL.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor httpContext;

    public CurrentUser(IHttpContextAccessor httpContext)
    {
        this.httpContext = httpContext;
    }

    public bool IsLoggedIn()
    {
        var id = httpContext.HttpContext?.Session.GetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME);
        return id != null;
    }
}
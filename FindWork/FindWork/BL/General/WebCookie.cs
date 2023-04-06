using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FindWork.BL.General;

public class WebCookie : IWebCookie
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public WebCookie(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public void AddSecure(string cookieName, string value)
    {
        var options = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            Secure = true
        };

        httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Add(string cookieName, string value)
    {
        var options = new CookieOptions();
        options.Path = "/";

        httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Delete(string cookieName)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookieName);
    }

    public string? Get(string cookieName)
    {
        var cookie = (httpContextAccessor.HttpContext?.Request.Cookies).FirstOrDefault(i => i.Key == cookieName);
        // if (cookie is not null && !string.IsNullOrEmpty(cookie.Value.Value))
            // return cookie.Value.Value;
        
        return null;
    }
}
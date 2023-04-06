using System;
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

    public void AddSecure(string cookieName, string value, int days = 0)
    {
        var options = new CookieOptions();
        options.Path = "/";
        options.HttpOnly = true;
        options.Secure = true;
        if (days > 0)
            options.Expires = DateTimeOffset.UtcNow.AddDays(days);

        httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Add(string cookieName, string value, int days = 0)
    {
        var options = new CookieOptions();
        options.Path = "/";
        if (days > 0)
            options.Expires = DateTimeOffset.UtcNow.AddDays(days);

        httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Delete(string cookieName)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookieName);
    }

    public string? Get(string cookieName)
    {
        var cookie = httpContextAccessor.HttpContext?.Request.Cookies
            .FirstOrDefault(i => i.Key == cookieName);
        
        if (cookie is not null && !string.IsNullOrEmpty(cookie.Value.Value))
            return cookie.Value.Value;
        
        return null;
    }
}
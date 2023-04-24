using FindWork.BL.General;

namespace FindWork.Test.Helpers;

public class TestWebCookie : IWebCookie
{
    private readonly Dictionary<string, string> cookies = new();
    public void AddSecure(string cookieName, string value, int days = 0)
    {
        cookies.Add(cookieName, value);
    }

    public void Add(string cookieName, string value, int days = 0)
    {
        cookies.Add(cookieName, value);
    }

    public void Delete(string cookieName)
    {
        cookies.Remove(cookieName);
    }

    public string? Get(string cookieName)
    {
        return cookies.ContainsKey(cookieName) ? cookies[cookieName] : null;
    }

    public void Clear() => cookies.Clear();
}
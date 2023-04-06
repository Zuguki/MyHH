namespace FindWork.BL.General;

public interface IWebCookie
{
    void AddSecure(string cookieName, string value);
    
    void Add(string cookieName, string value);
    
    void Delete(string cookieName);
    
    string? Get(string cookieName);
}
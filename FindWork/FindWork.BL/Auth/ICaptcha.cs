using System.Threading.Tasks;

namespace FindWork.BL.Auth;

public interface ICaptcha
{
    string? SiteKey { get; }

    Task<bool> ValidateToken(string token);
}
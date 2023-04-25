using System.Threading.Tasks;

namespace FindWork.BL.Auth;

public class DevCaptcha : ICaptcha
{
    public string SiteKey => "";
    
    public Task<bool> ValidateToken(string token) =>
        Task.FromResult(true);
}
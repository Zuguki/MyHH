using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindWork.BL.Auth;

public class GoogleCaptcha : ICaptcha
{
    private readonly string? secret;
    private readonly HttpClient client = new();

    public GoogleCaptcha(string? siteKey, string? secret)
    {
        SiteKey = siteKey;
        this.secret = secret;
    }

    public string? SiteKey { get; }

    public async Task<bool> ValidateToken(string token)
    {
        const string url = "https://www.google.com/recaptcha/api/siteverify";
        var responseMessage = await client.GetAsync($"{url}?secret={secret}&response={token}");

        if (responseMessage.StatusCode != HttpStatusCode.OK)
            return false;

        var json = await responseMessage.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<GoogleTokenResult>(json);

        return tokenResponse?.success == true;
    }

    private class GoogleTokenResult
    {
        public bool success { get; set; }
    }
}

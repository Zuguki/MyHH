using FindWork.BL.Auth;
using FindWork.DAL.Models;
using FindWork.Test.Helpers;
using FluentAssertions;

namespace FindWork.Test;

public class CurrentUserTest : BaseTest
{
    [Test]
    public async Task BaseRegistrationTest()
    {
        using (var scope = Helper.CreateTransactionScope())
        {
            await CreateAndAuthUser(true);

            var isLoggedIn = await currentUser.IsLoggedIn();
            isLoggedIn.Should().BeTrue();

            webCookie.Delete(AuthConstants.SessionCookieName);
            session.ResetSessionCache();

            isLoggedIn = await currentUser.IsLoggedIn();
            isLoggedIn.Should().BeTrue();

            webCookie.Delete(AuthConstants.SessionCookieName);
            webCookie.Delete(AuthConstants.RememberMeCookieName);
            session.ResetSessionCache();

            isLoggedIn = await currentUser.IsLoggedIn();
            isLoggedIn.Should().BeFalse();
        }
    }

    private async Task<int> CreateAndAuthUser(bool rememberMe)
    {
        var email = Guid.NewGuid() + "@test.com";
        var userId = await auth.CreateUser(
            new UserModel()
            {
                Email = email,
                Password = "Qwer!234"
            });
        
        return await auth.Authenticate(email, "Qwer!234", rememberMe);
    }
}
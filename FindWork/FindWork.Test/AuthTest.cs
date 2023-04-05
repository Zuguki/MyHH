using FindWork.BL.Exceptions;
using FindWork.DAL.Models;
using FindWork.Test.Helpers;
using FluentAssertions;

namespace FindWork.Test;

public class AuthTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        using (var scope = Helper.CreateTransactionScope())
        {
            var email = Guid.NewGuid() + "@test.com";

            var userId = await authBl.CreateUser(
                new UserModel()
                {
                    Email = email,
                    Password = "Some123Password#",
                });

            var invalidEmailAndPassword = () =>
            {
                authBl.Authenticate("sese", "fdas", false).GetAwaiter().GetResult();
            };
            var invalidEmail = () =>
            {
                authBl.Authenticate("sese", "Some123Password#", false).GetAwaiter().GetResult();
            };
            var invalidPassword = () =>
            {
                authBl.Authenticate(email, "fdas", false).GetAwaiter().GetResult();
            };
            
            invalidEmailAndPassword.Should().Throw<AuthorizeException>();
            invalidEmail.Should().Throw<AuthorizeException>();
            invalidPassword.Should().Throw<AuthorizeException>();
            
            // correct
            await authBl.Authenticate(email, "Some123Password#", false);
        }
    }
}
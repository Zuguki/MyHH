using System.Transactions;
using FindWork.DAL.Models;
using FindWork.Test.Helpers;
using FluentAssertions;

namespace FindWork.Test;

public class RegistrationTest : BaseTest
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
            var validateEmailResult = await authBl.ValidateEmail(email);
            
            // validate: user is not created
            validateEmailResult.Should().BeNull();

            var user = new UserModel()
            {
                Email = email,
                Password = "Some123Password#",
            };
            
            var userId = await authBl.CreateUser(user);
            validateEmailResult = await authBl.ValidateEmail(email);
            
            // validate: user is created
            validateEmailResult.Should().NotBeNull();
        }
    }
}
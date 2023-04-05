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
            
            // validate: user is not created
            var validateEmailResult = await authBl.ValidateEmail(email);
            validateEmailResult.Should().BeNull();

            // create user
            var userId = await authBl.CreateUser(
                new UserModel()
                {
                    Email = email,
                    Password = "Some123Password#",
                });

            userId.Should().BeGreaterThan(0);

            var userDalById = await authDal.GetUser(userId);
            userDalById.Email.Should().BeEquivalentTo(email);
            userDalById.Salt.Should().NotBeNull();

            var userDalByEmail = await authDal.GetUser(email);
            userDalByEmail.Email.Should().BeEquivalentTo(email);
            userDalByEmail.Salt.Should().NotBeNull();
            
            
            // validate: user is created
            validateEmailResult = await authBl.ValidateEmail(email);
            validateEmailResult.Should().NotBeNull();

            encrypt.HashPassword("Some123Password#", userDalByEmail.Salt).Should()
                .BeEquivalentTo(userDalByEmail.Password);
        }
    }
}
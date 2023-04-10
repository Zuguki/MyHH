using System.Transactions;
using FindWork.BL.Exceptions;
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
    public async Task RegisterTest()
    {
        using (var scope = Helper.CreateTransactionScope())
        {
            var email = Guid.NewGuid() + "@test.com";
            
            // validate: user is not created
            var act = () =>
            {
                auth.ValidateEmail(email).GetAwaiter().GetResult();
            };
            act.Should().NotThrow<DuplicateEmailException>();

            // create user
            var userId = await auth.CreateUser(
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
            act = () =>
            {
                auth.ValidateEmail(email).GetAwaiter().GetResult();
            };
            act.Should().Throw<DuplicateEmailException>();

            encrypt.HashPassword("Some123Password#", userDalByEmail.Salt).Should()
                .BeEquivalentTo(userDalByEmail.Password);
        }
    }
}
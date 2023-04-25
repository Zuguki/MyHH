using System;
using System.Threading.Tasks;
using FindWork.BL.Email;
using FindWork.DAL;
using FindWork.DAL.Models;

namespace FindWork.BL.Auth;

public class UserSecurity : IUserSecurity
{
    private readonly IUserSecurityDAL userSecurityDAL;
    private readonly IEmailQueue emailQueue;

    public UserSecurity(IUserSecurityDAL userSecurityDAL, IEmailQueue emailQueue)
    {
        this.userSecurityDAL = userSecurityDAL;
        this.emailQueue = emailQueue;
    }

    public async Task CreateUserVerification(int userid, string email)
    {
        var model = new UserSecurityModel
        {
            UserId = userid,
            VerificationCode = Guid.NewGuid().ToString(),
        };
        
        await userSecurityDAL.AddUserSecurity(model);
        await emailQueue.EnqueueMessage(email, "Account verification", @$"
                 Please confirm your email http://localhost/account/verification/{model.VerificationCode}");
    }

    public async Task<UserSecurityModel> GetUserSecurity(int userid) =>
        await userSecurityDAL.GetUserSecurity(userid);
}
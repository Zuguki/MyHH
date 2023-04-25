using System;
using System.Threading.Tasks;
using FindWork.DAL.Email;
using FindWork.DAL.Models;

namespace FindWork.BL.Email;

public class EmailQueue : IEmailQueue
{
    private readonly IEmailQueueDAL emailQueueDal;

    public EmailQueue(IEmailQueueDAL emailQueueDal)
    {
        this.emailQueueDal = emailQueueDal;
    }

    public string From { get; set; } = "noreply.guskov@gmail.com";

    public async Task<int> EnqueueMessage(string email, string subject, string body)
    {
        return await emailQueueDal.Enqueue(
            new EmailQueueModel
            {
                EmailFrom = From,
                EmailTo = email,
                EmailSubject = subject,
                EmailBody = body,
                Created = DateTime.Now
            }
        );
    }
}
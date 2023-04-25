using System;
using FindWork.DAL.Email;

namespace FindWork.BL.Email;

public class EmailProcessor
{
    public static readonly IEmailQueueDAL queue = new EmailQueueDAL();

    public static readonly IEmailClient emailClient =
        new EmailClient("smtp.gmail.com", "noreply.guskov@gmail.com", "nmslqsneuehalauo");

    public static void Process(int emailsLimit)
    {
        foreach (var email in queue.Dequeue(emailsLimit).GetAwaiter().GetResult())
        {
            try
            {
                emailClient.SendEmail(email.EmailTo!, email.EmailFrom!, email.EmailSubject!, email.EmailBody!);
                queue.Delete(email.EmailQueueId).GetAwaiter().GetResult();
            }
            catch (Exception) 
            {
                queue.Retry(email.EmailQueueId).GetAwaiter().GetResult();
            }
        }
    } 
}
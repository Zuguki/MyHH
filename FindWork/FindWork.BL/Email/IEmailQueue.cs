using System.Threading.Tasks;

namespace FindWork.BL.Email;

public interface IEmailQueue
{
    Task<int> EnqueueMessage(string email, string subject, string body);
}
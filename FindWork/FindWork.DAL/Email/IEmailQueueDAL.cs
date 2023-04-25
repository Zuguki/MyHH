using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL.Email;

public interface IEmailQueueDAL
{
    Task<int> Enqueue(EmailQueueModel model);

    Task<IEnumerable<EmailQueueModel>> Dequeue(int emailsLimit);

    Task Delete(int emailQueueId);

    Task Retry(int emailQueueId); 
}
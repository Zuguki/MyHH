using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL.Email;

public class EmailQueueDAL : IEmailQueueDAL
{
    public async Task<int> Enqueue(EmailQueueModel model)
    {
        var sql = @"insert into EmailQueue(EmailTo, EmailFrom, EmailSubject, EmailBody, Created, Retry)
                    values(@EmailTo, @EmailFrom, @EmailSubject, @EmailBody, @Created, 0)
                    returning EmailQueueId";

        return await DbHelper.QueryScalarAsync<int>(sql, model);
    }

    public async Task<IEnumerable<EmailQueueModel>> Dequeue(int emailsLimit)
    {
        var guid = Guid.NewGuid();

        await DbHelper.ExecuteAsync(@$"
            update EmailQueue
            set ProcessingId = @id
            where EmailQueueId in (
                select EmailQueueId
                from EmailQueue
                where ProcessingId is not null and Retry < 5
                fetch first {emailsLimit} rows only)",
            new {id = guid});

        return await DbHelper.QueryAsync<EmailQueueModel>(@"
            select EmailQueueId, EmailTo, EmailFrom, EmailSubject, EmailBody, Created, ProcessingId, Retry
            from EmailQueue", new {id = guid});
    }

    public async Task Delete(int emailQueueId)
    { 
        await DbHelper.ExecuteAsync(@"
            delete from EmailQueue
            where EmailQueueId = @id", new {id = emailQueueId});
    }

    public async Task Retry(int emailQueueId)
    {
        await DbHelper.ExecuteAsync(@"
            update EmailQueue 
            set ProcessingId = null, Retry = Retry + 1 
            where EmailQueueId = @id", new {id = emailQueueId});
    }
}
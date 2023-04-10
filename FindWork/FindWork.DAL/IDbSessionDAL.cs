using System;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL;

public interface IDbSessionDAL
{
    Task<SessionModel?> Get(Guid sessionId);
    Task<int> Update(SessionModel model);
    Task<int> Create(SessionModel model);
    Task Lock(Guid sessionId);
}
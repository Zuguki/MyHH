using System;
using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL;

public interface IDbSessionDAL
{
    Task<SessionModel?> GetSession(Guid sessionId);
    Task<int> UpdateSession(SessionModel model);
    Task<int> CreateSession(SessionModel model);
}
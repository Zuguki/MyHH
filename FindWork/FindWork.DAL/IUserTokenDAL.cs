using System;
using System.Threading.Tasks;

namespace FindWork.DAL;

public interface IUserTokenDAL
{
    Task<Guid> Create(int userId);

    Task<int?> Get(Guid tokenId);
}
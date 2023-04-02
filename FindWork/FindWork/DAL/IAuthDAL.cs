using System.Threading.Tasks;
using FindWork.DAL.Models;

namespace FindWork.DAL;

public interface IAuthDAL
{
    Task<UserModel> GetUser(int id);
    Task<UserModel> GetUser(string email);
    Task<int> CreateUser(UserModel model);
}
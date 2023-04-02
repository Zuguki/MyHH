using System.Threading.Tasks;
using FindWork.DAL;
using FindWork.DAL.Models;

namespace FindWork.BL.Auth;

public class AuthBL : IAuthBL
{
    private readonly IAuthDAL authDal;
    
    public AuthBL(IAuthDAL authDal)
    {
        this.authDal = authDal;
    }
    
    public async Task<int> CreateUser(UserModel model) =>
        await authDal.CreateUser(model);
}
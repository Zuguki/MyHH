using FindWork.DAL.Models;
using FindWork.ViewModels;

namespace FindWork.ViewMapper;

public class AuthMapper
{
    public static UserModel MapRegistrationViewModelToUserModel(RegisterViewModel model)
    {
        return new UserModel
        {
            Email = model.Email!,
            Password = model.Password!,
        };
    }
}
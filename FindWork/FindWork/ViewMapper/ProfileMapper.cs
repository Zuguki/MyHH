using FindWork.DAL.Models;
using FindWork.ViewModels;

namespace FindWork.ViewMapper;

public class ProfileMapper
{
    public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
    {
        return new ProfileModel
        {
            ProfileName = model.ProfileName,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
    }
    
    public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
    {
        return new ProfileViewModel
        {
            ProfileId = model.ProfileId,
            ProfileName = model.ProfileName,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
    }
}
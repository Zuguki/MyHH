using System;
using System.Linq;
using System.Threading.Tasks;
using FindWork.BL.Auth;
using FindWork.BL.Profile;
using FindWork.Middleware;
using FindWork.Service;
using FindWork.ViewMapper;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

[SiteAuthorize]
public class ProfileController : Controller
{
    private readonly IProfile profile;
    private readonly ICurrentUser currentUser;

    public ProfileController(IProfile profile, ICurrentUser currentUser)
    {
        this.profile = profile;
        this.currentUser = currentUser;
    }
    
    [HttpGet]
    [Route("/profile")]
    public async Task<IActionResult> Index()
    {
        var profiles = await currentUser.GetProfiles();
        var profileModel = profiles.FirstOrDefault();

        var viewModel = profileModel is null 
            ? new ProfileViewModel() 
            : ProfileMapper.MapProfileModelToProfileViewModel(profileModel);
        
        return View("Index", viewModel);
    }
    
    [HttpPost]
    [Route("/profile")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(ProfileViewModel model)
    {
        var userId = await currentUser.GetUserId();
        if (userId is null)
            throw new Exception("User is not found");

        var profiles = await profile.Get((int) userId);
        if (model.ProfileId is not null && profiles.All(m => m.ProfileId != model.ProfileId))
            throw new Exception("Error");

        if (ModelState.IsValid)
        {
            var profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
            profileModel.UserId = (int) userId;
            if (Request.Form.Files.Count > 0)
            {
                var fileName = WebFileWorker.GetWebFileName(Request.Form.Files[0].FileName);
                await WebFileWorker.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), fileName, 800, 600);
                profileModel.ProfileImage = fileName;
            }
            
            await profile.AddOrUpdate(profileModel);
            return Redirect("/");
        }

        return View("Index", new ProfileViewModel());
    }
}
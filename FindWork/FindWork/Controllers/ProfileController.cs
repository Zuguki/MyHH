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
        var userId = await currentUser.GetUserId();

        if (userId is null)
            ModelState.TryAddModelError("Id", "User not authorized");

        if (!ModelState.IsValid)
            return View("Index", new ProfileViewModel());
        
        var profiles = await profile.Get((int) userId!);
        var currentProfile = profiles.FirstOrDefault();
        if (currentProfile is null)
            return View("Index", new ProfileViewModel());

        var viewModel = ProfileMapper.MapProfileModelToProfileViewModel(currentProfile);
        return View("Index", viewModel);
    }
    
    [HttpPost]
    [Route("/profile")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            throw new Exception("Model uncorrect");

        var imageData = Request.Form.Files[0];
        {
            var fileName = WebFileWorker.GetWebFileName(imageData.FileName);
            await WebFileWorker.UploadAndResizeImage(imageData.OpenReadStream(), fileName, 800, 600);
        }
        
        var profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
        profileModel.UserId = (int) (await currentUser.GetUserId())!;
        var profileId = await profile.Add(profileModel);

        // return View("Index", new ProfileViewModel());
        return Redirect("/");
    }
}
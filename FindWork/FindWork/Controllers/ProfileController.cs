using System.Threading.Tasks;
using FindWork.Middleware;
using FindWork.Service;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

[SiteAuthorize]
public class ProfileController : Controller
{
    [HttpGet]
    [Route("/profile")]
    public IActionResult Index()
    {
        return View("Index", new ProfileViewModel());
    }
    
    [HttpPost]
    [Route("/profile")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave()
    {
        var imageData = Request.Form.Files[0];
        {
            var fileName = WebFileWorker.GetWebFileName(imageData.FileName);
            await WebFileWorker.UploadAndResizeImage(imageData.OpenReadStream(), fileName, 800, 600);
        }

        return View("Index", new ProfileViewModel());
    }
}
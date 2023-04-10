using System.Threading.Tasks;
using FindWork.BL.Auth;
using FindWork.Middleware;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

[SiteNotAuthorize]
public class LoginController : Controller
{
    private readonly IAuth auth;
    
    public LoginController(IAuth auth)
    {
        this.auth = auth;
    }
    
    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
    {
        return View("Index", new LoginViewModel());
    }
    
    [HttpPost]
    [Route("/login")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await auth.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }
            catch
            {
                ModelState.AddModelError("Email", "Name or Email is not valid");
            }
        }
    
        return View("Index", model);
    }
}
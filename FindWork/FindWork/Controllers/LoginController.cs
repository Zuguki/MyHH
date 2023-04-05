using System.Threading.Tasks;
using FindWork.BL.Auth;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

public class LoginController : Controller
{
    private readonly IAuth _auth;
    
    public LoginController(IAuth auth)
    {
        this._auth = auth;
    }
    
    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
    {
        return View("Index", new LoginViewModel());
    }
    
    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _auth.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
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
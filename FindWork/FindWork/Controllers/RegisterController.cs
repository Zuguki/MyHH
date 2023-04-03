using System.Threading.Tasks;
using FindWork.BL.Auth;
using FindWork.ViewMapper;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

public class RegisterController : Controller
{
    private readonly IAuthBL authBl;

    public RegisterController(IAuthBL authBl)
    {
        this.authBl = authBl;
    }

    [HttpGet]
    [Route("/register")]
    public IActionResult Index()
    {
        return View("Index", new RegisterViewModel());
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            await authBl.CreateUser(AuthMapper.MapRegistrationViewModelToUserModel(model));
            return Redirect("/");
        }

        return View("Index", model);
    }
}
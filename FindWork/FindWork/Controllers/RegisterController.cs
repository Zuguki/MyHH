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
    public IActionResult IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            authBl.CreateUser(AuthMapper.MapRegistrationViewModelToUserModel(model));
            return Redirect("/");
        }

        return View("Index", model);
    }
}
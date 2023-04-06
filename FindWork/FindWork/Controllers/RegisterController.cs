using System.Threading.Tasks;
using FindWork.BL.Auth;
using FindWork.BL.Exceptions;
using FindWork.ViewMapper;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

public class RegisterController : Controller
{
    private readonly IAuth _auth;

    public RegisterController(IAuth auth)
    {
        this._auth = auth;
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
            try
            {
                await _auth.Register(AuthMapper.MapRegistrationViewModelToUserModel(model));
                return Redirect("/");
            }
            catch (DuplicateEmailException)
            {
                ModelState.TryAddModelError("Email", "Email was duplicated");
            }
        }

        return View("Index", model);
    }
}
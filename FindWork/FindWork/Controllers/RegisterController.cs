using System.Threading.Tasks;
using FindWork.BL.Auth;
using FindWork.BL.Exceptions;
using FindWork.Middleware;
using FindWork.ViewMapper;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

[SiteNotAuthorize]
public class RegisterController : Controller
{
    private readonly IAuth auth;
    private readonly ICaptcha captcha;

    public RegisterController(IAuth auth, ICaptcha captcha)
    {
        this.auth = auth;
        this.captcha = captcha;
    }

    [HttpGet]
    [Route("/register")]
    public IActionResult Index()
    {
        ViewBag.CaptchaSitekey = captcha.SiteKey!;
        return View("Index", new RegisterViewModel());
    }

    [HttpPost]
    [Route("/register")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        ViewBag.CaptchaSitekey = captcha.SiteKey!;
        var isCaptchaValid = await captcha.ValidateToken(Request.Form["g-recaptcha-response"]!);
        if (!isCaptchaValid)
            ModelState.TryAddModelError("captcha", "Incorrect Captcha");
        
        if (ModelState.IsValid)
        {
            try
            {
                await auth.Register(AuthMapper.MapRegistrationViewModelToUserModel(model));
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
using FindWork.BL.Email;
using FindWork.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

[SiteAuthorize]
public class MessageController : Controller
{
    [HttpGet]
    [Route("/getmessage")]
    public IActionResult Index()
    {
        EmailProcessor.Process(1);
        return Redirect("/");
    }
}
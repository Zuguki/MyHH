using System.Threading.Tasks;
using FindWork.BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.ViewComponents;

public class MainMenuViewComponent : ViewComponent
{
    private readonly ICurrentUser currentUser;

    public MainMenuViewComponent(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var isLoggedIn = await currentUser.IsLoggedIn();
        return View("MainMenu", isLoggedIn);
    }
}
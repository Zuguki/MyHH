using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FindWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindWork.Controllers;

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
    public async Task<IActionResult> IndexSave()
    {
        var imageData = Request.Form.Files[0];
        {
            var md5Hash = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(imageData.FileName);
            var hashBytes = md5Hash.ComputeHash(inputBytes);

            var hash = Convert.ToHexString(hashBytes);
            var dir = "./wwwroot/images/" + hash[..2] + "/" + hash[..4];
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var fileName = dir + "/" + imageData.FileName;
            using var stream = System.IO.File.Create(fileName);
            await imageData.CopyToAsync(stream);
        }

        return View("Index", new ProfileViewModel());
    }
}
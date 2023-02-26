using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;

namespace DWC_NightOwlProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;

    }

    public IActionResult Index()
    {
        bool isAthenticated = User.Identity.IsAuthenticated;
        string name = User.Identity.Name;
        string authType = User.Identity.AuthenticationType;
        
        

        string id = _userManager.GetUserId(User);

        ViewBag.Message = $"User {name} = is authenticated? {isAthenticated} using type {authType}, ID from Identity is {id}";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

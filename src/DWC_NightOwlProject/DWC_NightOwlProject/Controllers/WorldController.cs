using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using DWC_NightOwlProject.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DWC_NightOwlProject.Controllers;

public class WorldController : Controller
{
    private readonly ILogger<WorldController> _logger;

    public WorldController(ILogger<WorldController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(World w)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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

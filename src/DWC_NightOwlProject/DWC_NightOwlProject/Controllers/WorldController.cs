using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.ViewModel;
using System.Security.Claims;
using DWC_NightOwlProject.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DWC_NightOwlProject.Controllers;

public class WorldController : Controller
{
    private readonly ILogger<WorldController> _logger;
    private IWorldRepository worldRepo;

    public WorldController(ILogger<WorldController> logger, IWorldRepository repo)
    {
        _logger = logger;
        worldRepo=repo;
    }

    [Authorize]
    public IActionResult Index()
    {
        String userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        ViewModelWorld w=new ViewModelWorld();
        if(w.setWorld(worldRepo,userId))
        {
            return View(w);
        }
        return View();
    }

    [HttpPost]
    public IActionResult Index(ViewModelWorld w)
    {
        String userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(userId!=null)
        {
            World newWorld=new World();
            newWorld.UserId=userId;
            newWorld.CreationDate=DateTime.Now;
            newWorld.Name=w.WorldName;
            try
            {
                worldRepo.AddOrUpdate(newWorld);
                ViewBag.Message="World created";
                return View();
            }
            catch(DbUpdateConcurrencyException e)
            {
                ViewBag.Message = "An unknown database error occurred while trying to create the item.  Please try again.";
                return View();
            }
        }
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

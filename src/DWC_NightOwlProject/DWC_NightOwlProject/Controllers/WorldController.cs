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
        private readonly UserManager<IdentityUser> _userManager;

    public WorldController(ILogger<WorldController> logger, IWorldRepository repo, UserManager<IdentityUser> um)
    {
        _userManager=um;
        _logger = logger;
        worldRepo=repo;
    }

    [Authorize]
    public IActionResult Index()
    {
        String userId = _userManager.GetUserId(User);
        ViewModelWorld vmw=new ViewModelWorld();
        World userWorld=getUserWorld(userId);
        if(userWorld!=null)
        {
            vmw.ThisWorld=userWorld;
            return View(vmw);
        }
        return View();
    }

    [HttpPost]
    public IActionResult Index(ViewModelWorld w)
    {
        String userId = _userManager.GetUserId(User);
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
    

    public World getUserWorld(string userid)
    {
        World w=worldRepo.GetUserWorld(userid);
        if(w!=null)
        {
            return w;
        }
        return null;
    }
}

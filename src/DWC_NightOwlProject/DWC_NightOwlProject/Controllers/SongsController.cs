using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Authorization;
using OpenAI;
using OpenAI.Images;
using System.Net;


namespace DWC_NightOwlProject.Controllers;

public class SongsController : Controller
{
    private ISongRepository _songRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private IWebHostEnvironment _environment;
    public SongsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IWebHostEnvironment environment,
                                   ISongRepository songRepository)
    {
        _songRepository = songRepository;
        _config = config;
        _userManager = userManager;
        _environment = environment;
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();


        string id = _userManager.GetUserId(User);
        var result = new List<Song>();
        result = _songRepository.GetAllSongsById(id);

        vm.songs = result;
        return View(vm);
    }


}
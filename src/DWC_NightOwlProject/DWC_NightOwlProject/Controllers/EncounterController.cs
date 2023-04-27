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
using OpenAI;

namespace DWC_NightOwlProject.Controllers;

public class EncounterController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMaterialRepository _materialRepository;
    private IWorldRepository worldRepo;
    private readonly IConfiguration _config;
    private readonly string materialType="Encounter";

    public EncounterController(UserManager<IdentityUser> um, IMaterialRepository materialRepository,IWorldRepository wRepo, IConfiguration config)
    {
        _userManager = um;
        _materialRepository = materialRepository;
        worldRepo=wRepo;
        _config=config;
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();
        string id = _userManager.GetUserId(User);
        var result = new List<Material>();
        result = _materialRepository.GetAllEncountersById(id);

        vm.materials = result;
        return View(vm);
    }

    public IActionResult EncounterForm()
    {
        return View();
    }

    public async Task<IActionResult> Completion(EncounterViewModel encounter)
    {
        var key=_config["APIKey"];
        var api=new OpenAIClient(new OpenAIAuthentication(key));
        var result = await api.CompletionsEndpoint.CreateCompletionAsync(encounter.Prompt(), max_tokens: 1000, temperature: .5, presencePenalty: .5, frequencyPenalty: .5, model: OpenAI.Models.Model.Davinci);
        encounter.Result=result.ToString();
        return View(encounter);
    }

    public IActionResult Save(EncounterViewModel evm)
    {
        String userId = _userManager.GetUserId(User);
        World userWorld=getUserWorld(userId);
        Material m= new Material();
        m.Type=materialType;
        m.UserId=userId;
        m.WorldId=userWorld.Id;
        m.Name=evm.Description();
        m.CreationDate=DateTime.Now;
        m.Prompt=evm.Prompt();
        m.Completion=evm.Description()+evm.Result;
        if(userId!=null)
        {
            try
            {
                _materialRepository.AddOrUpdate(m);
                ViewBag.Message="Encounter saved";
                return RedirectToAction("Index");
            }
            catch(DbUpdateConcurrencyException e)
            {
                ViewBag.Message = "An unknown database error occurred while trying to create the item.  Please try again.";
                return RedirectToAction("Index");
            }
        }
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public World getUserWorld(string userid)
    {
        World w = worldRepo.GetUserWorld(userid);
        if (w != null)
        {
            return w;
        }
        return null;
    }
}
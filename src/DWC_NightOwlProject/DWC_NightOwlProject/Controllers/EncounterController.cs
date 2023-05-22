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
    private readonly IEncounterRepository encounterRepository;
    private readonly string materialType="Encounter";

    public EncounterController(UserManager<IdentityUser> um, IMaterialRepository materialRepository,IWorldRepository wRepo, IConfiguration config,IEncounterRepository encounterrepo)
    {
        _userManager = um;
        _materialRepository = materialRepository;
        worldRepo=wRepo;
        _config=config;
        encounterRepository=encounterrepo;
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();
        string id = _userManager.GetUserId(User);
        var result = new List<Encounter>();
        result = encounterRepository.GetAllEncountersById(id);

        vm.encounters = result;
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
        Encounter e= new Encounter();
        e.Type=evm.Type;
        e.Biome=evm.Biome;
        e.UserId=userId;
        e.WorldId=userWorld.Id;
        //e.Name=evm.Description();
        e.CreationDate=DateTime.Now;
        e.Prompt=evm.Prompt();
        e.Completion=evm.Description()+evm.Result;
        if(userId!=null)
        {
            try
            {
                encounterRepository.AddOrUpdate(e);
                ViewBag.Message="Encounter saved";
                return RedirectToAction("Index");
            }
            catch(DbUpdateConcurrencyException ex)
            {
                ViewBag.Message = "An unknown database error occurred while trying to create the item.  Please try again.";
                return RedirectToAction("Index");
            }
        }
        return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var encounter = new Encounter();
        encounter= encounterRepository.GetEncounterByIdandMaterialId(userId, id);
        if(encounter!=null)
        {
            return View(encounter);
        }
        return View("Index");
    }
    public IActionResult SubmitEdit(Encounter e)
    {
        try
        {
            encounterRepository.AddOrUpdate(e);
        }
        catch
        {
            return View("Index");
        }
        return View("Index");
    }
        public IActionResult EnterNew()
        {
            return View();
        }
        public IActionResult SubmitNew(UserInputViewModel uvm)
        {
            if(uvm.IsValid())
            {
                var userId = _userManager.GetUserId(User);
                var e = new Encounter();
                e.Biome="Undetermined";
                e.Type="Undetermined";
                e.UserId = userId;
                e.Id = 0;
                e.CreationDate = DateTime.Now;
                e.Prompt = "";
                e.Completion = uvm.Sanitize(uvm.UserInput);
                encounterRepository.AddOrUpdate(e);
            }
            return View("Index");
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
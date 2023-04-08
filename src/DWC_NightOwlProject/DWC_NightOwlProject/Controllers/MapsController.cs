using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Authorization;
using OpenAI;
using OpenAI.Images;
using System.Security.Policy;

namespace DWC_NightOwlProject.Controllers;

public class MapsController : Controller
{
    private IMaterialRepository _materialRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    public MapsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager)
    {
        _materialRepository = materialRepository;
        _config = config;
        _userManager = userManager;
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();
       

        string id = _userManager.GetUserId(User);
        var result = new List<Material>();
        result = _materialRepository.GetAllMapsById(id);

        vm.materials = result;
        return View(vm);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Template(IFormCollection collection)
    {

        try
        {
            var vm = new MaterialVM();
            vm.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                        "The map should have a square grid overlaying it. "
                            + "It is: " + vm.r0
                            + ". The biome type is: " + vm.r1
                            + ". The map should have: " + vm.r2
                            + "squares.";





            string id = _userManager.GetUserId(User);
            var result = new List<Material>();
            result = _materialRepository.GetAllMapsById(id);
            vm.materials = result;
            
            return RedirectToAction("Completion","Maps", vm);
        }

        catch
        {
            return View();
        }

       
    }

    [Authorize]
    [HttpPost]
    public ActionResult Scratch(IFormCollection collection)
    {

        try
        {
            var vm = new MaterialVM();
            vm.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                        "The map should have a square grid overlaying it. " + vm.r3;





            string id = _userManager.GetUserId(User);
            var result = new List<Material>();
            result = _materialRepository.GetAllMapsById(id);
            vm.materials = result;
            return RedirectToAction("Completion", "Maps", vm);
        }

        catch
        {
            return View();
        }


    }



    [Authorize]
    public async Task<ActionResult> Completion(MaterialVM vm)
    {

        var userId = _userManager.GetUserId(User);


        var material = new Material();

        material.UserId = userId;
        material.Id = 0;
        material.Type = "Map";
        material.Name = "Name";
        material.CreationDate = DateTime.Now;
        material.Prompt = vm.Prompt;
        material.Prompt += "...";

        var APIKey = _config["APIKey"];
        var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
        var mapList = await api.ImagesEndPoint.GenerateImageAsync(material.Prompt, 1, ImageSize.Large);
        var map = mapList.FirstOrDefault();
        var result = map.ToString();
        

        material.Completion = result;

        ViewBag.Prompt = vm.Prompt;

        return View(material);

    }

    public ActionResult Save(Material material)
    {
        _materialRepository.AddOrUpdate(material);
        return RedirectToAction("Index", material);
    }

    // GET: HomeController1/Details/5
    public ActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetMapByIdandMaterialId(userId, id);

        return View(material);
    }

    // GET: HomeController1/Edit/5
    public ActionResult Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetMapByIdandMaterialId(userId, id);
        return View(material);
    }

    // POST: HomeController1/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var material = new Material();
            material = _materialRepository.GetMapByIdandMaterialId(userId, id);
            material.Name = Request.Form["Name"].ToString();
            _materialRepository.AddOrUpdate(material);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: HomeController1/Delete/5
    public ActionResult Delete(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetMapByIdandMaterialId(userId, id);
        return View(material);
    }

    // POST: HomeController1/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var material = new Material();
            material = _materialRepository.GetMapByIdandMaterialId(userId, id);
            _materialRepository.Delete(material);


            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

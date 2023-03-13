using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Authorization;
using OpenAI;
using OpenAI.Images;

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
    public IActionResult Index(string r0, string r1, string r2, string r3, string r4, string r5)
    {
        var vm = new MaterialVM();
        var responses = new List<string>();
        responses.Add(r0);
        responses.Add(r1);
        responses.Add(r2);
        responses.Add(r3);
        responses.Add(r4);
        responses.Add(r5);

        vm.Responses= responses;


        string id = _userManager.GetUserId(User);
        var result = new List<Material>();
        result = _materialRepository.GetAllCharactersById(id);

        vm.materials = result;
        return View(vm);
    }

   

    [Authorize]
    public async Task<ActionResult> Completion()
    {
        var userId = _userManager.GetUserId(User);


        var material = new Material();

        material.UserId = userId;
        material.Id = 0;
        material.Type = "Character";
        material.Name = "";
        material.CreationDate = DateTime.Now;
        material.Prompt = TempData.Peek("HoldPrompt").ToString();
        material.Prompt += "...";

        var APIKey = _config["APIKey"];
        var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
        var characterList = await api.ImagesEndPoint.GenerateImageAsync(material.Prompt, 1, ImageSize.Small);
        var character = characterList.FirstOrDefault();
        var result = character.ToString();

        material.Completion = result;
        ViewBag.Completion = result;
        TempData["HoldCompletion"] = material.Completion;



        return View(material);

    }

    public ActionResult Save()
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material.UserId = userId;
        material.Id = 0;
        material.Type = "Character";
        material.Name = "";
        material.CreationDate = DateTime.Now;
        material.Prompt = TempData.Peek("HoldPrompt").ToString();
        material.Prompt += "...";
        material.Completion = TempData.Peek("HoldCompletion").ToString();

        _materialRepository.AddOrUpdate(material);
        return RedirectToAction("Index", material);
    }

    // GET: HomeController1/Details/5
    public ActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetCharacterByIdandMaterialId(userId, id);

        return View(material);
    }

    // GET: HomeController1/Edit/5
    public ActionResult Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetCharacterByIdandMaterialId(userId, id);
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
            material = _materialRepository.GetCharacterByIdandMaterialId(userId, id);
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
        material = _materialRepository.GetCharacterByIdandMaterialId(userId, id);
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
            material = _materialRepository.GetCharacterByIdandMaterialId(userId, id);
            _materialRepository.Delete(material);


            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

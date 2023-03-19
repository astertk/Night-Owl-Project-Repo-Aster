using System.Diagnostics;
using System.IO;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Authorization;
using OpenAI;
using OpenAI.Images;
using OpenAI.Files;
using DWC_NightOwlProject.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Routing.Constraints;

namespace DWC_NightOwlProject.Controllers;

public class CharacterController : Controller
{
    private IMaterialRepository _materialRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IWebHostEnvironment _he;
    public CharacterController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IWebHostEnvironment he)
    {
        _materialRepository = materialRepository;
        _config = config;
        _userManager = userManager;
        _he = he;
        CharacterOptions.ConfigureFeatures();
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();
        string id = _userManager.GetUserId(User);
        var result = new List<Material>();
        result = _materialRepository.GetAllCharactersById(id);

        vm.materials = result;
        return View(vm);
    }

    [Authorize]
    public ActionResult Scratch(string fromScratch)
    {
        ViewBag.FromScratch = fromScratch;
        ViewBag.Prompt = " Create a Character for my Dungeons and Dragons Campaign:  " + ViewBag.FromScratch;
        TempData["HoldPrompt"] = ViewBag.Prompt;

        return View();
    }

    [Authorize]
    public async Task<ActionResult> Template(string cClass, string race, int age, string tone, double height, int weight)
    {
        
       /* class, race, age, tone, height, weight*/

      

            ViewBag.Class = cClass;
            ViewBag.Race = race;
            ViewBag.Age = age;
            ViewBag.Tone = tone;
            ViewBag.Height = height;
            ViewBag.Weight = weight;

        string prompt = " Character for my Dungeons and Dragons Campaign.  "
                        + "They are a: " + cClass
                        + ". Their race is: " + race
                        + ". Their age is: " + age.ToString()
                        + ". Their skin tone is: " + tone
                        + ". Their height in inches is: " + height.ToString()
                        + ". Their weight is: " + weight.ToString()
                        + " Only include the character. Do not include text or columns. Show the full body and face.";


            //ViewBag.Prompt = " Create a Dungeons and Dragons Backstory. Make the length of the backstory roughly " + ViewBag.MaxLength + " characters." + ViewBag.SuggestionOne + answerOne + ViewBag.SuggestionTwo + answerTwo + ViewBag.SuggestionThree + answerThree + ViewBag.SuggestionFour + answerFour;
            TempData["HoldPrompt"] = prompt;

        return View();
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
        var characterList =  await api.ImagesEndPoint.GenerateImageAsync(material.Prompt, 1, ImageSize.Small);
        var character = characterList.FirstOrDefault();
        var result = character.ToString();

        material.Completion = result;
        ViewBag.Completion = result;
        TempData["HoldCompletion"] = material.Completion;



        return View(material);

    }

    [Authorize]
    public ActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> UploadCompletion(IFormFile img)
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

        string webroot = _he.WebRootPath;
        var imgName = Path.GetFileName(img.FileName);
        //string extension = Path.GetExtension(img.FileName);
        //string fileName = imgName + DateTime.Now.ToString("yymmssfff") + extension;

        string path = Path.Combine(webroot + "/Image/", imgName);

        //using (var fileStream = new FileStream(path, FileMode.Create))
        //{
            //await img.CopyToAsync(fileStream);
        //}

        var APIKey = _config["APIKey"];
        var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
        var characterList = await api.ImagesEndPoint.CreateImageEditAsync(Path.GetFullPath(path), "C:\\Users\\jazzp\\Github\\Night-Owl-Project-Repo\\src\\DWC_NightOwlProject\\DWC_NightOwlProject\\wwwroot\\output-onlinepngtools.png", material.Prompt, 1, ImageSize.Small);
        var character = characterList.FirstOrDefault();
        var result = character.ToString();

        material.Completion = result;

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

    public IActionResult CharacterSheet(SheetRandomizer sr)
    {
        sr.Generate(sr.Race,sr.Class);
        return View(sr);
    }

    public IActionResult CreateSheet()
    {
        return View();
    }
    public ActionResult CreateSheetWithCharacter(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetCharacterByIdandMaterialId(userId, id);
        SheetRandomizer sr=new SheetRandomizer();
        sr.Character=material;
        return View("CreateSheet",sr);
    }
}

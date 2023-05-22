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
using DWC_NightOwlProject.DAL.Concrete;
using System.Net;

namespace DWC_NightOwlProject.Controllers;

public class CharacterController : Controller
{
    private IMaterialRepository _materialRepository;
    private ICharacterRepository _characterRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IWebHostEnvironment _he;
    public CharacterController(IMaterialRepository materialRepository, ICharacterRepository characterRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IWebHostEnvironment he)
    {
        _characterRepository= characterRepository;
        _materialRepository = materialRepository;
        _config = config;
        _userManager = userManager;
        _he = he;
        //CharacterOptions.ConfigureFeatures();
        CharacterOptions.ConfigureLists();
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();
        string id = _userManager.GetUserId(User);
        var result = new List<Character>();
        result = _characterRepository.GetAllCharactersById(id);

        vm.characters = result;
        return View(vm);
        return View();
    }

    [Authorize]
    public async Task<ActionResult> Scratch(IFormCollection collection)
    {
        var userId = _userManager.GetUserId(User);

        try
        {
            if (_characterRepository.GetAllCharactersById(userId).Count() < 4)
            {
                ViewBag.Error = "";


                var character = new Character();
                character.Prompt = "Create a Character for my Dungeons and Dragons Campaign. " +
                                    collection["r0"];


                character.UserId = userId;
                character.Id = 0;
                character.Name = "New Character";
                character.CreationDate = DateTime.Now;
                character.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var characterList = await api.ImagesEndPoint.GenerateImageAsync(character.Prompt, 1, ImageSize.Large);
                var newCharacter = characterList.FirstOrDefault();
                var url = newCharacter.ToString();
                character.Completion = url;
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                character.PictureData = webClient.DownloadData(url);
                _characterRepository.AddOrUpdate(character);
            }

            else
            {
                ViewBag.Error = "Too many Map Materials. Please delete 1 or more to create a new character!";
            }
        }

        catch
        {
            throw new Exception("Too many Character materials in Database");
        }

        return RedirectToAction("Index", "Character");
    }

    [Authorize]
    public async Task<ActionResult> Template(IFormCollection collection)
    {
        var userId = _userManager.GetUserId(User);


        if (_characterRepository.GetAllCharactersById(userId).Count() < 4)
        {
            ViewBag.Error = "";


            var character = new Character();
            character.Prompt = "Create a Character for my Dungeons and Dragons Campaign. "
                            + "Their class is  : " + collection["r0"]
                            + ". Their race is " + collection["r1"]
                            + ". Their age is " + collection["r2"]
                            + ". Their skin tone is " + collection["r3"]
                            + ". Their height is " + collection["r4"]
                            + ". Their weight is " + collection["r5"]
                            + "Only include the character. Do not include text or columns. Show the full body and face.";


            character.UserId = userId;
            character.Id = 0;
            character.Name = "New Character";
            character.CreationDate = DateTime.Now;
            character.Prompt += "...";

            var APIKey = _config["APIKey"];
            var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
            var characterList = await api.ImagesEndPoint.GenerateImageAsync(character.Prompt, 1, ImageSize.Large);
            var newCharacter = characterList.FirstOrDefault();
            var url = newCharacter.ToString();
            character.Completion = url;
            //string tempPath = Path.GetTempPath();

            // Scrape web page
            WebClient webClient = new WebClient();
            character.PictureData = webClient.DownloadData(url);
            _characterRepository.AddOrUpdate(character);
        }

        else
        {
            ViewBag.Error = "Too many Character Materials. Please delete 1 or more to create a new character!";
        }




        return RedirectToAction("Index", "Character");

    }
    [Authorize]
    public async Task<ActionResult> Random()
    {
        // Generate a random DND character name using the Faker library
        //var characterName = Faker.Name.First();named {characterName}

        /*var prompt = $"Create a DND character with a random race, age, skin tone, height, and weight. Only include the character. Do not include text or columns. Show the full body and face.";

        TempData["HoldPrompt"] = prompt;

        return View();*/
        var userId = _userManager.GetUserId(User);

        try
        {
            if (_characterRepository.GetAllCharactersById(userId).Count() < 4)
            {
                ViewBag.Error = "";

                var character = new Character();
                character.Prompt = "Create a Character for my Dungeons and Dragons Campaign. Show the full body and face.";

                character.UserId = userId;
                character.Id = 0;
                character.Name = "New Character";
                character.CreationDate = DateTime.Now;
                character.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var characterList = await api.ImagesEndPoint.GenerateImageAsync(character.Prompt, 1, ImageSize.Large);
                var newCharacter = characterList.FirstOrDefault();
                var url = newCharacter.ToString();
                character.Completion = url;

                WebClient webClient = new WebClient();
                character.PictureData = webClient.DownloadData(url);
                _characterRepository.AddOrUpdate(character);
            }
            else
            {
                ViewBag.Error = "Too many Map Materials. Please delete 1 or more to create a new character!";
            }
        }
        catch
        {
            throw new Exception("Too many Character materials in Database");
        }

        return RedirectToAction("Index", "Character");
    }

    [Authorize]
    public async Task<ActionResult> Completion(Character character)
    {
        return View(character);
    }

   

    [Authorize]
    public ActionResult Upload()
    {
        return View();
    }

    [Authorize]
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

        //string webroot = _he.WebRootPath;
        var imgName = Path.GetFileName(img.FileName);
        //string extension = Path.GetExtension(img.FileName);
        //string fileName = imgName + DateTime.Now.ToString("yymmssfff") + extension;

        //string path = Path.Combine(webroot + "/Image/", imgName);

        //using (var fileStream = new FileStream(path, FileMode.Create))
        //{
            //await img.CopyToAsync(fileStream);
        //}

        var APIKey = _config["APIKey"];
        var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
        var characterList = await api.ImagesEndPoint.CreateImageEditAsync(Path.GetFullPath(imgName), Path.GetFullPath(imgName), material.Prompt, 1, ImageSize.Small);
        var character = characterList.FirstOrDefault();
        var result = character.ToString();

        material.Completion = result;

        return View(material);
    }

    [Authorize]
    public ActionResult Save(Character character)
    {
        _characterRepository.AddOrUpdate(character);
        return RedirectToAction("Index" , "Character");
    }

    [Authorize]

    // GET: HomeController1/Details/5
    public ActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var character = new Character();
        character = _characterRepository.GetCharacterByIdandMaterialId(userId, id);
        
        return View(character);
    }

    // GET: HomeController1/Edit/5
    public ActionResult Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var character = new Character();
        character = _characterRepository.GetCharacterByIdandMaterialId(userId, id);
        return View(character);
    }

    [Authorize]
    // POST: HomeController1/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var character = new Character();
            character = _characterRepository.GetCharacterByIdandMaterialId(userId, id);
            character.Name = Request.Form["Name"].ToString();
            _characterRepository.AddOrUpdate(character);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize]
    // GET: HomeController1/Delete/5
    public ActionResult Delete(int id)
    {
        var userId = _userManager.GetUserId(User);
        var character = new Character();
        character = _characterRepository.GetCharacterByIdandMaterialId(userId, id);
        if (character == null)
        {
            return RedirectToAction("Index");
        }
        return View(character);
    }

    [Authorize]
    // POST: HomeController1/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var character = new Character();
            character = _characterRepository.GetCharacterByIdandMaterialId(userId, id);
            _characterRepository.Delete(character);


            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize]
    public IActionResult CharacterSheet(SheetRandomizer sr)
    {
        sr.Generate(sr.Race,sr.Class);
        return View(sr);
    }

    [Authorize]
    public IActionResult CreateSheet()
    {
        return View();
    }

    [Authorize]
    public ActionResult CreateSheetWithCharacter(int id)
    {
        var userId = _userManager.GetUserId(User);
        var character = new Character();
        character = _characterRepository.GetCharacterByIdandMaterialId(userId, id);
        SheetRandomizer sr=new SheetRandomizer();
        sr.Character=character;
        return View("CreateSheet",sr);
    }
}

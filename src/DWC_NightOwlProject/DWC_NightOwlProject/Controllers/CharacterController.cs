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

public class CharacterController : Controller
{
    private IMaterialRepository _materialRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    public CharacterController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager)
    {
        _materialRepository = materialRepository;
        _config = config;
        _userManager = userManager;
    }
    [Authorize]
    public IActionResult Index()
    {
        string id = _userManager.GetUserId(User);
        var result = new List<Material>();
        result = _materialRepository.GetAllCharactersById(id);
        return View(result);
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

        string prompt = " Create a Character for my Dungeons and Dragons Campaign with these attributes:  "
                        + "Class: " + cClass
                        + ". Race: " + race
                        + ". Age: " + age.ToString()
                        + ". Skin Tone: " + tone
                        + ". Height in Inches: " + height.ToString()
                        + ". Weight in Lbs: " + weight.ToString()
                        + ".";


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

    public ActionResult Save()
    {
        var userId = _userManager.GetUserId(User);

        var material = new Material();
        material.UserId = userId;
        material.Id = 0;
        material.Type = "Character";
        material.CreationDate = DateTime.Now;
        material.Prompt = TempData.Peek("HoldPrompt").ToString();
        material.Prompt += "...";
        material.Completion = TempData.Peek("HoldCompletion").ToString();

        _materialRepository.AddOrUpdate(material);
        return RedirectToAction("Index", material);
    }
}

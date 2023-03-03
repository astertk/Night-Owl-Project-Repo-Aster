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
    public ActionResult Scratch(string fromScratch, int maxLength, double temp, double presence, double frequency)
    {
        ViewBag.FromScratch = fromScratch;
        ViewBag.MaxLength = maxLength.ToString();
        ViewBag.Temp = temp;
        ViewBag.Presence = presence;
        ViewBag.Frequency = frequency;
        ViewBag.Prompt = " Create a Dungeons and Dragons Backstory.  " + ViewBag.FromScratch + " Make the length of the backstory roughly " + ViewBag.MaxLength + " characters.";
        TempData["HoldPrompt"] = ViewBag.Prompt;
        TempData["HoldTemp"] = temp.ToString();
        TempData["HoldPresence"] = presence.ToString();
        TempData["HoldFrequency"] = frequency.ToString();

        return View();
    }

    [Authorize]
    public async Task<ActionResult> Template(string answerOne, string answerTwo, string answerThree, string answerFour, int maxLength, double temp, double presence, double frequency)
    {
        
        if (User.Identity.IsAuthenticated)
        {



            ViewBag.AnswerOne = answerOne;
            ViewBag.AnswerTwo = answerTwo;
            ViewBag.AnswerThree = answerThree;
            ViewBag.AnswerFour = answerFour;
            ViewBag.MaxLength = maxLength.ToString();
            ViewBag.Temp = temp;
            ViewBag.Presence = presence;
            ViewBag.Frequency = frequency;
            ViewBag.SuggestionOne = " The overall tone is: ";
            ViewBag.SuggestionTwo = " The villains are: ";
            ViewBag.SuggestionThree = " The heros are: ";
            ViewBag.SuggestionFour = " The world is: ";
            ViewBag.Prompt = " Create a Dungeons and Dragons Backstory. Make the length of the backstory roughly " + ViewBag.MaxLength + " characters." + ViewBag.SuggestionOne + answerOne + ViewBag.SuggestionTwo + answerTwo + ViewBag.SuggestionThree + answerThree + ViewBag.SuggestionFour + answerFour;
            TempData["HoldPrompt"] = ViewBag.Prompt;

        }



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
        var character =  await api.ImagesEndPoint.GenerateImageAsync(material.Prompt, 1, ImageSize.Small);
        var result = character.ToString();

        material.Completion = result;
        ViewBag.Completion = result;
        TempData["HoldCompletion"] = material.Completion;



        return View(material);

    }

    public ActionResult Save()
    {
        var userId = _userManager.GetUserId(User);

        var backstoryCache = _materialRepository.GetAll().Where(x => x.UserId == userId).ToList();

        for (int i = 0; i < backstoryCache.Count; i++)
        {
            _materialRepository.Delete(backstoryCache[i]);
        }


        var material = new Material();
        material.UserId = userId;
        material.Id = 0;
        material.Type = "Backstory";
        material.CreationDate = DateTime.Now;
        material.Prompt = TempData.Peek("HoldPrompt").ToString();
        material.Prompt += "...";
        material.Completion = TempData.Peek("HoldCompletion").ToString();

        _materialRepository.AddOrUpdate(material);
        return RedirectToAction("Index", material);
    }
}

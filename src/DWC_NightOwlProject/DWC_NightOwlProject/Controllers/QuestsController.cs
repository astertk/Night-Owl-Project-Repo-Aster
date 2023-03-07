using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using OpenAI;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;
using System.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NuGet.ProjectModel;
using DWC_NightOwlProject.DAL.Concrete;
using NuGet.Protocol;

namespace DWC_NightOwlProject.Controllers
{
    public class QuestsController : Controller
    {
        private IMaterialRepository _materialRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<World> _worldRepository;

        public QuestsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IRepository<Template> templateRepository,
                                   IRepository<World> worldRepository)
        {
            _materialRepository = materialRepository;
            _config = config;
            _userManager = userManager;
            _templateRepository = templateRepository;
            _worldRepository = worldRepository;
        }



        [Authorize]
        public ActionResult Index()
        {
            string id = _userManager.GetUserId(User);

            /*if (_materialRepository.GetBackstoryById(id) == null)
            {
                return View();
            }*/
            /*ViewBag.Backstory = _materialRepository.GetBackstoryById(id);*/

            var material = _materialRepository.GetBackstoryById(id);

            //ViewBag.Backstory = material?.Completion ?? "No Backstory Created Yet...";

            /*            var result = material?.Completion ?? "No Backstory Created Yet...";*/

            return View(material);
        }

        [Authorize]
        public ActionResult Scratch(string fromScratch, int maxLength, double temp, double presence, double frequency)
        {
            ViewBag.FromScratch = fromScratch;
            ViewBag.MaxLength = maxLength.ToString();
            ViewBag.Temp = temp;
            ViewBag.Presence = presence;
            ViewBag.Frequency = frequency;
            ViewBag.Prompt = " Create a Dungeons and Dragons Quest.  " + ViewBag.FromScratch + " Make the length of the backstory roughly " + ViewBag.MaxLength + " characters.";
            TempData["HoldPrompt"] = ViewBag.Prompt;
            TempData["HoldTemp"] = temp.ToString();
            TempData["HoldPresence"] = presence.ToString();
            TempData["HoldFrequency"] = frequency.ToString();

            return View();
        }
        // GET: QuestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestController/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Template(string answerOne, string answerTwo, string answerThree, string answerFour, int maxLength, double temp, double presence, double frequency)
        {
            /*            var template = new TemplateViewModel();*/
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
                ViewBag.SuggestionOne = " Are you looking for a combat-heavy adventure or a more story-driven one? ";
                ViewBag.SuggestionTwo = " Where are you starting your adventure and where is your destination? ";
                ViewBag.SuggestionThree = " Would you like a quest with a clear objective or are you open to more open-ended adventures where your choices have a greater impact on the outcome? ";
                ViewBag.SuggestionFour = " What kind of reward or outcome would you like to see at the end of the quest? Do you want a magical item, a large sum of gold, or perhaps some other kind of intangible reward like a boost to reputation or influence? ";
                ViewBag.Prompt = " Write me a DND quest where it is "
                    + answerOne + " where the players start at "
                    + answerTwo + " and are travelling to "
                    + answerThree + " . "
                    + " The reward is a " + answerFour + " . "
                    + " Make it " + ViewBag.MaxLength + " characters long. Make this have 3 acts and an epilogue. ";

                   
                

                TempData["HoldPrompt"] = ViewBag.Prompt;
                TempData["HoldTemp"] = temp.ToString();
                TempData["HoldPresence"] = presence.ToString();
                TempData["HoldFrequency"] = frequency.ToString();
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
            material.Type = "Backstory";
            material.CreationDate = DateTime.Now;
            material.Prompt = TempData.Peek("HoldPrompt").ToString();
            material.Prompt += "...";


            var temp = TempData.Peek("HoldTemp").ToString();
            var presence = TempData.Peek("HoldPresence").ToString(); ;
            var frequency = TempData.Peek("HoldFrequency").ToString(); ;



            var t = Convert.ToDouble(temp);
            var p = Convert.ToDouble(presence);
            var f = Convert.ToDouble(frequency);


            var APIKey = _config["APIKey"];
            var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
            var backstory = await api.CompletionsEndpoint.CreateCompletionAsync(material.Prompt, max_tokens: 1000, temperature: t, presencePenalty: p, frequencyPenalty: f, model: OpenAI.Models.Model.Davinci);
            /*var backstory = await api.CompletionsEndpoint.CreateCompletionAsync(material.Prompt, max_tokens: 1000, temperature: 0.8, presencePenalty: 0.1, frequencyPenalty: 0.1, model: OpenAI.Models.Model.Davinci);*/
            var result = backstory.ToString();

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
            material.Type = "Quest";
            material.CreationDate = DateTime.Now;
            material.Prompt = TempData.Peek("HoldPrompt").ToString();
            material.Prompt += "...";
            material.Completion = TempData.Peek("HoldCompletion").ToString();

            _materialRepository.AddOrUpdate(material);
            return RedirectToAction("Index", material);
        }

        // POST: QuestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

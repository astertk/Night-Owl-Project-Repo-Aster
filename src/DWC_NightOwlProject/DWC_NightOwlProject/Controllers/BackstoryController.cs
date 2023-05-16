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
    public class BackstoryController : Controller
    {
        private IMaterialRepository _materialRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<World> _worldRepository;
        private readonly IBackstoryRepository backstoryRepository;

        public BackstoryController(IMaterialRepository materialRepository, IConfiguration config, 
                                   UserManager<IdentityUser> userManager, IRepository<Template> templateRepository, 
                                   IRepository<World> worldRepository,IBackstoryRepository backstoryRepo)
        {
            _materialRepository = materialRepository;
            _config = config;
            _userManager = userManager;
            _templateRepository = templateRepository;
            _worldRepository = worldRepository;
            backstoryRepository=backstoryRepo;
        }

        // GET: HomeController1
        [Authorize]
        public ActionResult Index()
        {
            var vm = new MaterialVM();
            string id = _userManager.GetUserId(User);

            /*if (_materialRepository.GetBackstoryById(id) == null)
            {
                return View();
            }*/
            /*ViewBag.Backstory = _materialRepository.GetBackstoryById(id);*/

            var result = new Backstory();
            var list = backstoryRepository.GetAllBackstoriesById(id);
            if(list!=null)
            {
                result=list.First();
            }
            else
            {
                result=null;
            }

            //ViewBag.Backstory = material?.Completion ?? "No Backstory Created Yet...";

            /*            var result = material?.Completion ?? "No Backstory Created Yet...";*/
            vm.backstory = result;
            return View(vm);
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

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
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
                ViewBag.SuggestionOne = " The overall tone is: ";
                ViewBag.SuggestionTwo = " The villains are: ";
                ViewBag.SuggestionThree = " The heros are: ";
                ViewBag.SuggestionFour = " The world is: ";
                ViewBag.Prompt = " Create a Dungeons and Dragons Backstory. Make the length of the backstory roughly " + ViewBag.MaxLength + " characters." + ViewBag.SuggestionOne + answerOne + ViewBag.SuggestionTwo + answerTwo + ViewBag.SuggestionThree + answerThree + ViewBag.SuggestionFour + answerFour;
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

           



            var material = new Backstory();
            material.UserId = userId;
            material.Id = 0;
            material.Name = "";
            //material.Type = "Backstory";
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

            var backstoryCache = _materialRepository.GetAll().Where(x => x.UserId == userId).ToList();

            for (int i = 0; i < backstoryCache.Count; i++)
            {
                _materialRepository.Delete(backstoryCache[i]);
            }

           
            var material = new Backstory();
            material.UserId = userId;
            material.Name = "Backstory";
            material.Id = 0;
            material.Name = "";
            //material.Type = "Backstory";
            material.CreationDate = DateTime.Now;
            material.Prompt = TempData.Peek("HoldPrompt").ToString();
            material.Prompt += "...";
            material.Completion = TempData.Peek("HoldCompletion").ToString();

            backstoryRepository.AddOrUpdate(material);
            return RedirectToAction("Index", material);
        }



        // POST: HomeController1/Create
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

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
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

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var material = new Backstory();
            material = backstoryRepository.GetAllBackstoriesById(userId).First();
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
                material = _materialRepository.GetBackstoryById(userId);
                _materialRepository.Delete(material);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /* public Template BuildTemplate(Template template)
         {
             template = new Template();
             template.Id = 0;
             template.CreationDate = DateTime.Today;
             template.Type = "Backstory Template";

             return template;
         }*/
    }
}

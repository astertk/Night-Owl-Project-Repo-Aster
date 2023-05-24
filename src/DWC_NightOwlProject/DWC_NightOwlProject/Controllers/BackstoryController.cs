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
using OpenAI.Images;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DWC_NightOwlProject.Controllers
{
    public class BackstoryController : Controller
    {
        private IMaterialRepository _materialRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<World> _worldRepository;
        private readonly IBackstoryRepository _backstoryRepository;

        public BackstoryController(IMaterialRepository materialRepository, IConfiguration config, 
                                   UserManager<IdentityUser> userManager, IRepository<Template> templateRepository, 
                                   IRepository<World> worldRepository,IBackstoryRepository backstoryRepo)
        {
            _materialRepository = materialRepository;
            _config = config;
            _userManager = userManager;
            _templateRepository = templateRepository;
            _worldRepository = worldRepository;
            _backstoryRepository=backstoryRepo;
        }

        // GET: HomeController1
        [Authorize]
        public IActionResult Index()
        {
            var vm = new MaterialVM();


            string id = _userManager.GetUserId(User);
            var result = new List<Backstory>();
            result = _backstoryRepository.GetAllBackstoriesById(id);

            vm.backstories = result;
            return View(vm);
        }
        [Authorize]
        public async Task<ActionResult> Scratch(IFormCollection collection)
        {

            var userId = _userManager.GetUserId(User);

            try
            {
                if (_backstoryRepository.GetAllBackstoriesById(userId).Count() < 4)
                {
                    ViewBag.Error = "";


                    var backstory = new Backstory();
                    backstory.Prompt = "Create a Backstory for my Dungeons and Dragons Campaign. " + collection["r0"] +
                                        ". Make the length of the backstory roughly" + collection["r1"] + "characters";


                    backstory.UserId = userId;
                    backstory.Id = 0;
                    backstory.Name = "New Backstory";
                    backstory.CreationDate = DateTime.Now;
                    backstory.Prompt += "...";

                    var APIKey = _config["APIKey"];
                    var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                    var newBackstory = await api.CompletionsEndpoint.CreateCompletionAsync(backstory.Prompt, max_tokens: 1000, temperature: 2, presencePenalty: 0, frequencyPenalty: 0, model: OpenAI.Models.Model.Davinci);
                    backstory.Completion = newBackstory.ToString();
                    _backstoryRepository.AddOrUpdate(backstory);

                }

               
            }

            catch
            {
                throw new Exception("Too many materials in Database");
            }

            return RedirectToAction("Index", "Backstory");

        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var map = new Backstory();
            map = _backstoryRepository.GetBackstoryByIdandMaterialId(userId, id);

            return View(map);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View(); 
        }

        [Authorize]
        public async Task<ActionResult> Template(IFormCollection collection)
        {
            var userId = _userManager.GetUserId(User);


            if (_backstoryRepository.GetAllBackstoriesById(userId).Count() < 6)
            {
                ViewBag.Error = "";


                var backstory = new Backstory();
                backstory.Prompt = "Create a Dungeons and Dragons Backstory. Make the length of the backstory roughly "
                                  + collection["r4"]
                                  + ". Make the tone of the story" + collection["r0"]
                                  + ". Make the villains of the story" + collection["r1"]
                                  + ". Make the heroes of the story" + collection["r2"]
                                  + ". Overall, the world is: " + collection["r3"]+
                                  ". Make sure to use proper punctuation and grammar.";




                backstory.UserId = userId;
                backstory.Id = 0;
                backstory.Name = "New Backstory";
                backstory.CreationDate = DateTime.Now;
                backstory.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var backstoryList = await api.ImagesEndPoint.GenerateImageAsync(backstory.Prompt, 1, ImageSize.Large);
                var newBackstory = await api.CompletionsEndpoint.CreateCompletionAsync(backstory.Prompt, max_tokens: 1000, temperature: 2, presencePenalty: 1, frequencyPenalty: 1, model: OpenAI.Models.Model.Davinci);
                backstory.Completion = newBackstory.ToString();
                _backstoryRepository.AddOrUpdate(backstory);

            }





            return RedirectToAction("Index", "Backstory");

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

            _backstoryRepository.AddOrUpdate(material);
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
            var backstory = new Backstory();
            backstory = _backstoryRepository.GetBackstoryByIdandMaterialId(userId, id);
            return View(backstory);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var backstory = new Backstory();
                backstory = _backstoryRepository.GetBackstoryByIdandMaterialId(userId, id);
                _backstoryRepository.Delete(backstory);


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

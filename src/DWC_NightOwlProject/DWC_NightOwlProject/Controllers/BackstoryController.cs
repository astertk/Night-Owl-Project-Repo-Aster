using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using OpenAI;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;


namespace DWC_NightOwlProject.Controllers
{
    public class BackstoryController : Controller
    {
        private IMaterialRepository _materialRepository;
        private readonly IConfiguration _config;

        public BackstoryController(IMaterialRepository materialRepository, IConfiguration config)
        {
            _materialRepository = materialRepository;
            _config = config;
        }

        // GET: HomeController1
        public ActionResult Index()
        {
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

        public async Task<ActionResult> Template(string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            /*            var template = new TemplateViewModel();*/


            ViewBag.AnswerOne = answerOne;
            ViewBag.AnswerTwo = answerTwo;
            ViewBag.AnswerThree = answerThree;
            ViewBag.AnswerFour = answerFour;
            ViewBag.SuggestionOne = "The overall tone is: ";
            ViewBag.SuggestionTwo = "The villains are: ";
            ViewBag.SuggestionThree = "The heros are: ";
            ViewBag.SuggestionFour = "The world is: ";
            ViewBag.Prompt = "Create a Dungeons and Dragons Backstory." + ViewBag.SuggestionOne + answerOne + ViewBag.SuggestionTwo + answerTwo + ViewBag.SuggestionThree + answerThree + ViewBag.SuggestionFour + answerFour;
            TempData["HoldPrompt"] = ViewBag.Prompt;


            return View();
        }
        public async Task<ActionResult> Completion(TemplateViewModel template)
        {

            var material = new Material();
            material.Id = 0;
            material.Type = "Backstory";
            material.CreationDate = DateTime.Now;
            material.Prompt = TempData.Peek("HoldPrompt").ToString();
            material.Prompt += "...";

            var APIKey = _config["APIKey"];
            var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
            var backstory = await api.CompletionsEndpoint.CreateCompletionAsync(material.Prompt, max_tokens: 1000, temperature: 0.8, presencePenalty: 0.1, frequencyPenalty: 0.1, model: OpenAI.Models.Model.Davinci);
            var result = backstory.ToString();

            material.Completion = result;
            ViewBag.Completion = result;

            return View(material);

        }

        public async Task<string> BuildCompletion(string completion)
        {
            var APIKey = _config["APIKey"];
            var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
            var backstory = await api.CompletionsEndpoint.CreateCompletionAsync("Create my background story for my Dungeons and Dragons Campaign. Theme: Comical. Mogarr the Loser wants to steal all the music from the realm!", max_tokens: 200, temperature: 0.8, presencePenalty: 0.1, frequencyPenalty: 0.1, model: OpenAI.Models.Model.Davinci);
            var result = backstory.ToString();

            return result;
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
            return View();
        }

        // POST: HomeController1/Delete/5
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

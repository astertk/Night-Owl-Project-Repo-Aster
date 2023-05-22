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
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.ViewModel;

namespace DWC_NightOwlProject.Controllers
{
    public class QuestsController : Controller
    {
        private IMaterialRepository _materialRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<World> _worldRepository;
        private readonly IQuestRepository questRepository;
        private readonly IBackstoryRepository backstoryRepo;
        private readonly string materialType="Quest";
        private IWorldRepository worldRepo;

        public QuestsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IRepository<Template> templateRepository,
                                   IRepository<World> worldRepository, IWorldRepository wRepo, IQuestRepository qRepo, IBackstoryRepository back)
        {
            _materialRepository = materialRepository;
            _config = config;
            _userManager = userManager;
            _templateRepository = templateRepository;
            _worldRepository = worldRepository;
            worldRepo=wRepo;
            questRepository=qRepo;
            backstoryRepo=back;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ContinueGuidedQuest(GuidedQuestViewModel qm)
        {
            GuidedQuestViewModel prevModel;
            var key=_config["APIKey"];
            var api=new OpenAIClient(new OpenAIAuthentication(key));
            if(qm.CurrentStep==null)
            {
                ViewBag.Message= "Must provide step input.";
                return View(qm);
            }
            else
            {
                var result = await api.CompletionsEndpoint.CreateCompletionAsync(qm.LatestStep(), max_tokens: 1000, temperature: .5, presencePenalty: .5, frequencyPenalty: .5, model: OpenAI.Models.Model.Davinci);
                qm.AddResult(result.ToString());
                return View(qm);
            }
        }

        [HttpPost]
        public IActionResult SaveGuidedQuest(GuidedQuestViewModel qm)
        {
            String userId = _userManager.GetUserId(User);
            //World userWorld=getUserWorld(userId);
            Quest q= new Quest();
            //q.Type=materialType;
            q.UserId=userId;
            //q.WorldId=userWorld.Id;
            q.Completion=qm.Results;
            q.Name="";
            q.Prompt="";
            q.Id=0;
            q.CreationDate=DateTime.Now;
            if(userId!=null)
            {
                try
                {
                    questRepository.AddOrUpdate(q);
                    //ViewBag.Message("Quest saved");
                    return View("Index");
                }
                catch(DbUpdateConcurrencyException e)
                {
                    ViewBag.Message = "An unknown database error occurred while trying to create the item.  Please try again.";
                    return View("ContinueGuidedQuest", qm);
                }
            }
            return View("ContinueGuidedQuest", qm);
        }
        
        [Authorize]
        public IActionResult GuidedQuestTemplate()
        {
            return View();
        }
        public World getUserWorld(string userid)
        {
            World w = worldRepo.GetUserWorld(userid);
            if (w != null)
            {
                return w;
            }
            return null;
        }
        [Authorize]
        public IActionResult CreateWithReference()
        {
            string id = _userManager.GetUserId(User);
            ReferenceSelector rs=new ReferenceSelector();
            rs.Backstories=backstoryRepo.GetAllBackstoriesById(id);
            return View(rs);
        }
        public async Task<IActionResult> ReferenceCompletion(ReferenceSelector rs)
        {
            string id = _userManager.GetUserId(User);
            var key=_config["APIKey"];
            var api=new OpenAIClient(new OpenAIAuthentication(key));
            var result = await api.CompletionsEndpoint.CreateCompletionAsync(rs.promptQuest(), max_tokens: 1000, temperature: .5, presencePenalty: .5, frequencyPenalty: .5, model: OpenAI.Models.Model.Davinci);
            rs.evm.Result=result.ToString();
            var q = new Quest();
            q.UserId = id;
            q.Id = 0;
            q.Name = "";
            //q.Type = "Quest";
            q.CreationDate = DateTime.Now;
            q.Prompt = "";
            q.Completion = result.ToString();

            return View("Completion",q);
        }

        [Authorize]
        public IActionResult Index()
        {
            var vm = new MaterialVM();
            string id = _userManager.GetUserId(User);
            var result = new List<Quest>();
            result = questRepository.GetAllQuestsById(id);

            vm.quests = result;
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
            ViewBag.Prompt = " Create a Dungeons and Dragons Quest.  " + ViewBag.FromScratch + " Make the length of the quest roughly " + ViewBag.MaxLength + " characters.";
            TempData["HoldPrompt"] = ViewBag.Prompt;
            TempData["HoldTemp"] = temp.ToString();
            TempData["HoldPresence"] = presence.ToString();
            TempData["HoldFrequency"] = frequency.ToString();

            return View();
        }
        // GET: QuestController/Details/5
        public ActionResult Details(int id)
        {
            string userId = _userManager.GetUserId(User);
            var material = new Quest();
            material = questRepository.GetQuestByIdandMaterialId(userId,id);
            return View(material);
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
                ViewBag.SuggestionTwo = " Where are you starting your adventure? ";
                ViewBag.SuggestionThree = " Where is your destination? ";
                ViewBag.SuggestionFour = " What kind of reward or outcome would you like to see at the end of the quest? Do you want a magical item, a large sum of gold, or perhaps some other kind of intangible reward like a boost to reputation or influence? ";
                ViewBag.Prompt = " Write me a DND quest where it is "
                    + answerOne + " where the players start at "
                    + answerTwo + " and are travelling to "
                    + answerThree + " . "
                    + " The reward is a " + answerFour + " . "
                    + " Make it approximately " + ViewBag.MaxLength + " characters long. Make this have 3 acts and an epilogue. ";

                   
                

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





            var material = new Quest();
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

        public IActionResult EnterNew()
        {
            return View();
        }
        public IActionResult SubmitNew(UserInputViewModel uvm)
        {
            if(uvm.IsValid())
            {
                var userId = _userManager.GetUserId(User);
                var q = new Quest();
                q.UserId = userId;
                q.Id = 0;
                q.Name = "";
                q.CreationDate = DateTime.Now;
                q.Prompt = "";
                q.Completion = uvm.Sanitize(uvm.UserInput);
                questRepository.AddOrUpdate(q);
            }
            return View("Index");
        }

        public ActionResult Save()
        {
            var userId = _userManager.GetUserId(User);

            var q = new Quest();
            q.UserId = userId;
            q.Id = 0;
            q.Name = "";
            //q.Type = "Quest";
            q.CreationDate = DateTime.Now;
            q.Prompt = TempData.Peek("HoldPrompt").ToString();
            q.Prompt += "...";
            q.Completion = TempData.Peek("HoldCompletion").ToString();

            questRepository.AddOrUpdate(q);
            return RedirectToAction("Index", q);
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
            var userId = _userManager.GetUserId(User);
            var quest = new Quest();
            quest= questRepository.GetQuestByIdandMaterialId(userId, id);
            if(quest!=null)
            {
                return View(quest);
            }
            return View("Index");
        }
        public IActionResult SubmitEdit(Quest q)
        {
            try
            {
                questRepository.AddOrUpdate(q);
            }
            catch
            {
                return View("Index");
            }
            return View("Index");
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
            string userId = _userManager.GetUserId(User);
            var material = new Quest();
            material = questRepository.GetQuestByIdandMaterialId(userId, id);
            return View(material);
        }

        // POST: QuestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                var material = new Quest();
                material = questRepository.GetQuestByIdandMaterialId(userId, id);
                questRepository.Delete(material);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

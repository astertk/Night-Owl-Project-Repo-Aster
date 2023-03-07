using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.ViewModel;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OpenAI;

namespace DWC_NightOwlProject.Controllers
{
    public class QuestsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private IMaterialRepository _materialRepository;
        private IWorldRepository worldRepo;
        private readonly string materialType="Quest";

        public QuestsController(IConfiguration config, UserManager<IdentityUser> um, IMaterialRepository mrepo,IWorldRepository wRepo)
        {
            _config=config;
            _userManager=um;
            _materialRepository=mrepo;
            worldRepo=wRepo;
        }

        // GET: QuestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult GuidedQuestTemplate()
        {
            return View();
        }

        public ActionResult ContinueGuidedQuest()
        {
            return View();
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
            World userWorld=getUserWorld(userId);
            Material m= new Material();
            m.Type=materialType;
            m.UserId=userId;
            m.WorldId=userWorld.Id;
            m.Completion=qm.Results;
            if(userId!=null)
            {
                try
                {
                    _materialRepository.AddOrUpdate(m);
                    ViewBag.Message("Quest saved");
                    return View("ContinueGuidedQuest", qm);
                }
                catch(DbUpdateConcurrencyException e)
                {
                    ViewBag.Message = "An unknown database error occurred while trying to create the item.  Please try again.";
                    return View("ContinueGuidedQuest", qm);
                }
            }
            return View("ContinueGuidedQuest", qm);
        }

/**        [HttpPost]
       public async Task<ActionResult> StartGuidedQuest(GuidedQuestViewModel quest)
        {
            var key=_config["APIKey"];
            var api=new OpenAIClient(new OpenAIAuthentication(key));
            ContinueGuidedQuestViewModel qm=new ContinueGuidedQuestViewModel();
            qm.QuestDetails=quest;
            bool success=qm.QuestDetails.AddStep();
            if(!success)
            {
                ViewBag.Message= "Must provide step input. Cannot have more than " +GuidedQuestViewModel.stepMax+" steps.";
                return View("ContinueGuidedQuest",qm);
            }
            else
            {
                var result = await api.CompletionsEndpoint.CreateCompletionAsync(qm.QuestDetails.LatestStep(), max_tokens: 1000, temperature: .5, presencePenalty: .5, frequencyPenalty: .5, model: OpenAI.Models.Model.Davinci);
                qm.QuestDetails.AddResult(result.ToString());
                return View("ContinueGuidedQuest",qm);
            }
        }**/

        // GET: QuestController/Create
        public World getUserWorld(string userid)
        {
            World w=worldRepo.GetUserWorld(userid);
            if(w!=null)
            {
                return w;
            }
            return null;
        }
        public ActionResult Create()
        {
            return View();
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

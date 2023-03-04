using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.Controllers
{
    public class QuestsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private IMaterialRepository _materialRepository;
        private readonly string materialType="Quest";

        public QuestsController(IConfiguration config, UserManager<IdentityUser> um, IMaterialRepository mrepo)
        {
            _config=config;
            _userManager=um;
            _materialRepository=mrepo;
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

        public ActionResult GuidedQuestTemplate()
        {
            return View();
        }

        // GET: QuestController/Create
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

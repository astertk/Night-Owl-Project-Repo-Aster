using OpenAI_API;
using OpenAI_API.Models;
using OpenAI_API.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Concrete;

namespace DWC_NightOwlProject.Controllers
{
    public class BackstoryController : Controller
    {
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

        // GET: Backstory/Template
        public ActionResult Template(string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            ViewBag.AnswerOne = answerOne;
            ViewBag.AnswerTwo = answerTwo;
            ViewBag.AnswerThree = answerThree;
            ViewBag.AnswerFour = answerFour;
            ViewBag.SuggestionOne = "The overall tone is:";
            ViewBag.SuggestionTwo = "The villains are:";
            ViewBag.SuggestionThree = "The heros are:";
            ViewBag.SuggestionFour = "The world is:";

            ViewBag.Prompt = "Create a Dungeons and Dragons Backstory." + "The overall tone is:" + answerOne + ". The villains are:" + answerTwo + ". The heros are:" + answerThree + " .The world is:" + answerFour;
            return View();
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

        public Template BuildTemplate(Template template)
        {
            template = new Template();
            template.Id = 0;
            template.CreationDate = DateTime.Today;
            template.Type = "Backstory Template";

            return template;
        }
    }
}

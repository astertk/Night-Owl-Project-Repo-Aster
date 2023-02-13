using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Models;
using OpenAI_API.Files;
using DWC_NightOwlProject.Models;

namespace DWC_NightOwlProject.Controllers
{
    public class BackstoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

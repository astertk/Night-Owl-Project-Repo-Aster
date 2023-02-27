using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;

namespace DWC_NightOwlProject.Controllers;

public class CharacterController : Controller
{

    public CharacterController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.ViewModel;
using System.Security.Claims;
using DWC_NightOwlProject.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DWC_NightOwlProject.Controllers;

public class WorldController : Controller
{
    private readonly ILogger<WorldController> _logger;
    private IWorldRepository worldRepo;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapRepository _mapRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IQuestRepository _questRepository;
    private readonly IBackstoryRepository _backstoryRepository;
    private readonly ISongRepository _songRepository;
    private readonly IItemRepository _itemRepository;


    public WorldController(ILogger<WorldController> logger, IWorldRepository repo, UserManager<IdentityUser> um, IMaterialRepository materialRepository, IMapRepository mapRepository,
                            IBackstoryRepository backstoryRepo, ICharacterRepository characterRepo, IQuestRepository questRepo, ISongRepository songRepo, IItemRepository itemRepo)
    {
        _userManager = um;
        _logger = logger;
        worldRepo = repo;
        _materialRepository = materialRepository;
        _mapRepository = mapRepository;
        _questRepository = questRepo;
        _backstoryRepository = backstoryRepo;
        _characterRepository = characterRepo;
        _songRepository= songRepo;
        _itemRepository = itemRepo;
    }

    [Authorize]
    public IActionResult Index()
    {
        String userId = _userManager.GetUserId(User);
        ViewModelWorld vmw = new ViewModelWorld();
        World userWorld = getUserWorld(userId);

        var backstory = new List<Backstory>();
        var quests = new List<Quest>();
        var characters = new List<Character>();
        var maps = new List<Map>();
        var songs = new List<Song>();
        var items = new List<Item>();
        backstory = _backstoryRepository.GetAllBackstoriesById(userId);
        quests = _questRepository.GetAllQuestsById(userId);
        characters = _characterRepository.GetAllCharactersById(userId);
        maps = _mapRepository.GetAllMapsById(userId);
        songs = _songRepository.GetAllSongsById(userId);
        items = _itemRepository.GetAllItemsById(userId);


        if (userWorld != null)
        {
            vmw.ThisWorld = userWorld;
            vmw.Backstory = backstory;

            if (backstory == null)
            {
                ViewBag.Backstory = "Backstory has not been created yet...";
            }

            vmw.quests = quests;

            if (quests == null)
            {
                ViewBag.Quests = "Quests have not been created yet...";
            }

            vmw.characters = characters;

            if (characters == null)
            {
                ViewBag.Characters = "Character(s) have not been created yet...";
            }

            vmw.maps = maps;

            if(maps == null)
            {
                ViewBag.Maps = "Map(s) have not been created yet...";
            }

            vmw.Songs = songs;

            if(songs == null)
            {
                ViewBag.Songs = "Song(s) have not been created yet...";
            }

            vmw.items = items;

            if(items == null)
            {
                ViewBag.Items = "Item(s) have not been created yet...";
            }

            return View(vmw);
        }
        else
        {
            return View();
        }
    }

    [HttpPost]
    public IActionResult Index(ViewModelWorld w)
    {
        String userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            World newWorld = new World();
            newWorld.UserId = userId;
            newWorld.CreationDate = DateTime.Now;
            newWorld.Name = w.WorldName;
            try
            {
                worldRepo.AddOrUpdate(newWorld);
                ViewBag.Message = "World created";
                return View();
            }
            catch (DbUpdateConcurrencyException e)
            {
                ViewBag.Message = "An unknown database error occurred while trying to create the item.  Please try again.";
                return View();
            }
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
}
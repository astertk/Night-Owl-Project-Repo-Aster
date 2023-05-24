using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWC_NightOwlProject.Models;
using Microsoft.AspNetCore.Identity;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.Data;
using Microsoft.AspNetCore.Authorization;
using OpenAI;
using OpenAI.Images;
using System.Net;
using DWC_NightOwlProject.ViewModel;

namespace DWC_NightOwlProject.Controllers;

public class MapsController : Controller
{
    private IMaterialRepository _materialRepository;
    private IMapRepository _mapRepository;
    private IBackstoryRepository backstoryRepo;
    private IQuestRepository questRepo;
    private IEncounterRepository encounterRepo; 
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private IWebHostEnvironment _environment;
    public MapsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IWebHostEnvironment environment,
                                   IMapRepository mapRepository, IBackstoryRepository back, IQuestRepository quest,IEncounterRepository encounter)
    {
        _materialRepository = materialRepository;
        _mapRepository = mapRepository;
        _config = config;
        _userManager = userManager;
        _environment = environment;
        backstoryRepo=back;
        questRepo=quest;
        encounterRepo=encounter;
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();


        string id = _userManager.GetUserId(User);
        var result = new List<Map>();
        result = _mapRepository.GetAllMapsById(id);

        vm.maps = result;
        return View(vm);
    }



    [Authorize]
    public async Task<ActionResult> Template(IFormCollection collection)
    {

        var userId = _userManager.GetUserId(User);

        
            if (_mapRepository.GetAllMapsById(userId).Count() < 4)
            {
                ViewBag.Error = "";


                var map = new Map();
                map.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                            "The map should have a square grid overlaying it. "
                                + "It is: " + collection["r0"]
                                + ". The biome type is: " + collection["r1"]
                                + ". The map should have: " + collection["r2"];


                map.UserId = userId;
                map.Id = 0;
                map.Name = "New Map";
                map.CreationDate = DateTime.Now;
                map.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var mapList = await api.ImagesEndPoint.GenerateImageAsync(map.Prompt, 1, ImageSize.Large);
                var newMap = mapList.FirstOrDefault();
                var url = newMap.ToString();
                map.Completion = url;
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                map.PictureData = webClient.DownloadData(url);
                _mapRepository.AddOrUpdate(map);
            }

            else
            {
                ViewBag.Error = "Too many Map Materials. Please delete 1 or more to create a new map!";
            }
        

      

        return RedirectToAction("Index", "Maps");

    }

    [Authorize]
    public async Task<ActionResult> Scratch(IFormCollection collection)
    {

        var userId = _userManager.GetUserId(User);

        try
        {
            if (_mapRepository.GetAllMapsById(userId).Count() < 4)
            {
                ViewBag.Error = "";


                var map = new Map();
                map.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                                    "The map should have a square grid overlaying it. " + collection["r0"];


                map.UserId = userId;
                map.Id = 0;
                map.Name = "New Map";
                map.CreationDate = DateTime.Now;
                map.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var mapList = await api.ImagesEndPoint.GenerateImageAsync(map.Prompt, 1, ImageSize.Large);
                var newMap = mapList.FirstOrDefault();
                var url = newMap.ToString();
                map.Completion = url;
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                map.PictureData = webClient.DownloadData(url);
                _mapRepository.AddOrUpdate(map);
            }

            else
            {
                ViewBag.Error = "Too many Map Materials. Please delete 1 or more to create a new map!";
            }
        }

        catch
        {
            throw new Exception("Too many map materials in Database");
        }

        return RedirectToAction("Index", "Maps");

    }
    [Authorize]
    public IActionResult CreateWithReference()
    {
        string id = _userManager.GetUserId(User);
        ReferenceSelector rs=new ReferenceSelector();
        rs.Backstories=backstoryRepo.GetAllBackstoriesById(id);
        rs.Quests=questRepo.GetAllQuestsById(id);
        rs.Encounters=encounterRepo.GetAllEncountersById(id);
        return View(rs);
    }
    [Authorize]
    public async Task<ActionResult> ReferenceCompletion(ReferenceSelector rs)
    {

        var userId = _userManager.GetUserId(User);

        try
        {
            if (_mapRepository.GetAllMapsById(userId).Count() < 4)
            {
                ViewBag.Error = "";


                var map = new Map();
                map.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                                    "The map should have a square grid overlaying it. "+rs.promptMap;


                map.UserId = userId;
                map.Id = 0;
                map.Name = "New Map";
                map.CreationDate = DateTime.Now;
                map.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var mapList = await api.ImagesEndPoint.GenerateImageAsync(map.Prompt, 1, ImageSize.Large);
                var newMap = mapList.FirstOrDefault();
                var url = newMap.ToString();
                map.Completion = url;
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                map.PictureData = webClient.DownloadData(url);
                _mapRepository.AddOrUpdate(map);
            }

            else
            {
                ViewBag.Error = "Too many Map Materials. Please delete 1 or more to create a new map!";
            }
        }

        catch
        {
            throw new Exception("Too many map materials in Database");
        }

        return RedirectToAction("Index", "Maps");

    }


    [Authorize]
    public async Task<ActionResult> Completion(Map map)
    {


        return View(map);

    }



    [Authorize]
    public async Task<ActionResult> EditCompletion(MaterialVM vm)
    {


        var userId = _userManager.GetUserId(User);

        var map = new Map();

        map.UserId = userId;
        map.Id = 0;
        map.Name = "Map";
        map.CreationDate = DateTime.Now;
        map.Prompt = vm.Prompt;
        map.Prompt += "...";

        //ImageConverter imageConverter = new ImageConverter();
        //byte[] vmByte = (byte[])imageConverter.ConvertTo(vm.upload, typeof(byte[]));
        //ByteArrayContent imageByteArrayContent = new ByteArrayContent(vmByte);
        //ByteArrayContent imageByteArrayContent = new ByteArrayContent(vm.PictureData);

        //string fileName = vm.upload.FileName.ToString();
        string fileName = vm.FileName; //"C:\\Users\\Jade\\Desktop\\dev\\CS461\\Night-Owl-Project-Repo\\src\\DWC_NightOwlProject\\DWC_NightOwlProject\\wwwroot\\css\\Forest.png";
        string prompt = vm.Prompt;
        string size = "1024x1024";
        var APIKey = _config["APIKey"];



        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + APIKey);

            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(prompt), "prompt");
                content.Add(new StringContent("1"), "n");
                content.Add(new StringContent(size), "size");
                content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(fileName)), "image", fileName);

                HttpResponseMessage response = client.PostAsync("https://api.openai.com/v1/images/edits", content).Result;
                var body = response.Content.ReadAsStringAsync().Result;
                body.ToString();

                var bodyArray = body.Split('\"');

                //The split string from the HttpResponseMessageBody, this is only the url. Don't change the 7 value.
                var result = bodyArray[7];

                map.Completion = result;


            }
        }





        return View(map);

    }

    public ActionResult Save(Map map)
    {
        _mapRepository.AddOrUpdate(map);
        return RedirectToAction("Maps/Index");
    }

    // GET: HomeController1/Details/5
    public ActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var map = new Map();
        map = _mapRepository.GetMapByIdandMaterialId(userId, id);

        return View(map);
    }


    // GET: HomeController1/Edit/5
    public ActionResult Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var map = new Map();
        map = _mapRepository.GetMapByIdandMaterialId(userId, id);
        return View(map);
    }

    // POST: HomeController1/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var map = new Map();
            map = _mapRepository.GetMapByIdandMaterialId(userId, id);
            map.Name = Request.Form["Name"].ToString();
            _mapRepository.AddOrUpdate(map);
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
        var map = new Map();
        map = _mapRepository.GetMapByIdandMaterialId(userId, id);
        return View(map);
    }

    // POST: HomeController1/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var map = new Map();
            map = _mapRepository.GetMapByIdandMaterialId(userId, id);
            _mapRepository.Delete(map);


            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

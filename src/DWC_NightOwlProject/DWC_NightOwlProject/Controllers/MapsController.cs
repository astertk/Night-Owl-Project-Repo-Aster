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


namespace DWC_NightOwlProject.Controllers;

public class MapsController : Controller
{
    private IMaterialRepository _materialRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private IWebHostEnvironment _environment;
    public MapsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IWebHostEnvironment environment)
    {
        _materialRepository = materialRepository;
        _config = config;
        _userManager = userManager;
        _environment = environment;
    }
    [Authorize]
    public IActionResult Index()
    {
        var vm = new MaterialVM();


        string id = _userManager.GetUserId(User);
        var result = new List<Material>();
        result = _materialRepository.GetAllMapsById(id);

        vm.materials = result;
        return View(vm);
    }

    /*public IActionResult Template()
    {
      
        return RedirectToAction("Index", "Maps");
    }*/
    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        string userId = _userManager.GetUserId(User);
        var material = new Material();

        material.UserId = userId;
        material.Id = 0;
        material.Type = "MapPreview";
        material.Name = "New Map";
        material.CreationDate = DateTime.Now;
        material.Prompt = " ";
        material.Prompt += "...";

        material.Completion = " ";

        _materialRepository.AddOrUpdate(material);


        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            material.PictureData = fileBytes;

        }



        _materialRepository.AddOrUpdate(material);
        return View();
    }


    public async Task<IActionResult> OnPostUploadAsync()
    {

        var vm = new MaterialVM();

        using (var memoryStream = new MemoryStream())
        {
            //await vm.Upload.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB


            vm.PictureData = memoryStream.ToArray();



            return View();
        }
    }


    [Authorize]
    public async Task<ActionResult> Template(IFormCollection collection)
    {

        var userId = _userManager.GetUserId(User);

        try
        {
            if (_materialRepository.GetAllMapsById(userId).Count() < 4)
            {
                ViewBag.Error = "";


                var material = new Material();
                material.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                            "The map should have a square grid overlaying it. "
                                + "It is: " + collection["r0"]
                                + ". The biome type is: " + collection["r1"]
                                + ". The map should have: " + collection["r2"]
                                + " squares.";


                material.UserId = userId;
                material.Id = 0;
                material.Type = "Map";
                material.Name = "New Map";
                material.CreationDate = DateTime.Now;
                material.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var mapList = await api.ImagesEndPoint.GenerateImageAsync(material.Prompt, 1, ImageSize.Large);
                var map = mapList.FirstOrDefault();
                var url = map.ToString();
                material.Completion = url;
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                material.PictureData = webClient.DownloadData(url);
                _materialRepository.AddOrUpdate(material);
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
    public async Task<ActionResult> Scratch(IFormCollection collection)
    {

        var userId = _userManager.GetUserId(User);

        try
        {
            if (_materialRepository.GetAllMapsById(userId).Count() < 4)
            {
                ViewBag.Error = "";


                var material = new Material();
                material.Prompt = "Create a Map for my Dungeons and Dragons Campaign. " +
                                    "The map should have a square grid overlaying it. " + collection["r0"];


                material.UserId = userId;
                material.Id = 0;
                material.Type = "Map";
                material.Name = "New Map";
                material.CreationDate = DateTime.Now;
                material.Prompt += "...";

                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
                var mapList = await api.ImagesEndPoint.GenerateImageAsync(material.Prompt, 1, ImageSize.Large);
                var map = mapList.FirstOrDefault();
                var url = map.ToString();
                material.Completion = url;
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                material.PictureData = webClient.DownloadData(url);
                _materialRepository.AddOrUpdate(material);
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
    [HttpPost]
    public async Task<ActionResult> ImageEdit(IFormCollection collection, IFormFile file)
    {

        var userId = _userManager.GetUserId(User);
        //try
        // {
        var vm = new MaterialVM();
        vm.Prompt = "Edit my DnD Map: " + collection["r0"];

        //Create a MemoryStream from the uploaded file
        MemoryStream memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

        //Convert to a Byte Array
        vm.PictureData = memoryStream.ToArray();





        var material = new Material();

        material.UserId = userId;
        material.Id = 0;
        material.Type = "MapPreview";
        material.Name = "New Map";
        material.CreationDate = DateTime.Now;
        material.Prompt = vm.Prompt;
        material.Prompt += "...";
        material.PictureData = vm.PictureData;
        material.Completion = " ";

        _materialRepository.AddOrUpdate(material);






        return RedirectToAction("EditCompletion", "Maps", vm);

    }






    [Authorize]
    public async Task<ActionResult> Completion(Material material)
    {


        return View(material);

    }



    [Authorize]
    public async Task<ActionResult> EditCompletion(MaterialVM vm)
    {


        var userId = _userManager.GetUserId(User);

        var material = new Material();

        material.UserId = userId;
        material.Id = 0;
        material.Type = "Map";
        material.Name = "Map";
        material.CreationDate = DateTime.Now;
        material.Prompt = vm.Prompt;
        material.Prompt += "...";

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

                material.Completion = result;


            }
        }





        return View(material);

    }

    public ActionResult Save(Material material)
    {
        material.Type = "Map";
        _materialRepository.AddOrUpdate(material);
        return RedirectToAction("Maps/Index");
    }

    // GET: HomeController1/Details/5
    public ActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetMapByIdandMaterialId(userId, id);

        return View(material);
    }


    // GET: HomeController1/Edit/5
    public ActionResult Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var material = new Material();
        material = _materialRepository.GetMapByIdandMaterialId(userId, id);
        return View(material);
    }

    // POST: HomeController1/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var material = new Material();
            material = _materialRepository.GetMapByIdandMaterialId(userId, id);
            material.Name = Request.Form["Name"].ToString();
            _materialRepository.AddOrUpdate(material);
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
        var material = new Material();
        material = _materialRepository.GetMapByIdandMaterialId(userId, id);
        return View(material);
    }

    // POST: HomeController1/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var material = new Material();
            material = _materialRepository.GetMapByIdandMaterialId(userId, id);
            _materialRepository.Delete(material);


            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

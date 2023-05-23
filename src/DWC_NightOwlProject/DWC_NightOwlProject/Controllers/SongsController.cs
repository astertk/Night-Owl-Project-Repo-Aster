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
using DWC_NightOwlProject.DAL.Concrete;


namespace DWC_NightOwlProject.Controllers;

public class SongsController : Controller
{
    private ISongRepository _songRepository;
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private IWebHostEnvironment _environment;
    public SongsController(IMaterialRepository materialRepository, IConfiguration config,
                                   UserManager<IdentityUser> userManager, IWebHostEnvironment environment,
                                   ISongRepository songRepository)
    {
        _songRepository = songRepository;
        _config = config;
        _userManager = userManager;
        _environment = environment;
    }

    
    public IActionResult Index()
    {
        var vm = new MaterialVM();


        string id = _userManager.GetUserId(User);
        var result = new List<Song>();
        result = _songRepository.GetAllSongsById(id);

        vm.songs = result;
        return View(vm);
    }


    [Authorize]
    public async Task<IActionResult> Template(IFormCollection collection)
    {
        var userId = _userManager.GetUserId(User);


        if (_songRepository.GetAllSongsById(userId).Count() < 6)
        {
            //Getting the notes from OpenAI
            var APIKey = _config["APIKey"];
            var api = new OpenAIClient(new OpenAIAuthentication(APIKey));

            var song = new Song();
            song.Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes."
                         + "The tone of the song is: " + collection["r0"]
                         + ". The song will be used for " + collection["r1"]
                         + ". The speed of the song will be " + collection["r2"]
                         + ". Make it one string of notes in this format, with no " +
                         "commas or periods. Make it 32 notes or longer, but not longer than 100 notes. " +
                         "Give the song a clear progression of chorus, verse 1, chorus. Don't include the words chorus or verse 1, just the notes with" +
                         "Don't let the notes go higher than G6, but give the notes plenty of variety. Don't include sharp notes, like D4#. Let it be just notes separated by " +
                         "spaces in quotes (don't forget the ending double quote), e.g: \"C4 F5 Ab4 F5\"";
           song.UserId = userId;
            song.Id = 0;
            song.InstrumentId = Convert.ToInt32(collection["r3"]);
            song.RateId = Convert.ToInt32(collection["r2"]);

            var namePrompt = "Come up with a song name. The tone of the song is" + collection["r0"] + ". Make it Dungeons and Dragons inspired. Make it 1 to 3 words.";
            var gptName = await api.CompletionsEndpoint.CreateCompletionAsync(namePrompt, max_tokens: 5, temperature: 2, presencePenalty: 0, frequencyPenalty: 0, model: OpenAI.Models.Model.Davinci);
            song.Name = gptName.ToString();


            song.CreationDate = DateTime.Now;
            var notes = await api.CompletionsEndpoint.CreateCompletionAsync(song.Prompt, max_tokens: 2000, temperature: 1, presencePenalty: 0, frequencyPenalty: 0, model: OpenAI.Models.Model.Davinci);
            song.Completion = notes.ToString();

            var dallePrompt = "Make a cover art image of this song prompt. Make it crazy looking with a lot of imagery. Don't put any words or letters in the cover art. The tone of the coverart is " + collection["r0"];
            var dalleList = await api.ImagesEndPoint.GenerateImageAsync(dallePrompt, 1, ImageSize.Large);
            var dalleImage = dalleList.FirstOrDefault();
            var url = dalleImage.ToString();
            //string tempPath = Path.GetTempPath();

            // Scrape web page
            WebClient webClient = new WebClient();
            song.PictureData = webClient.DownloadData(url);
            _songRepository.AddOrUpdate(song);
        }


        return RedirectToAction("Index", "Songs");
    }

    [Authorize]
    // GET: HomeController1/Details/5
    public ActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var song = new Song();
        song = _songRepository.GetSongByIdandMaterialId(userId, id);

        return View(song);
    }

    [Authorize]
    // GET: HomeController1/Delete/5
    public ActionResult Delete(int id)
    {
        var userId = _userManager.GetUserId(User);
        var song = new Song();
        song = _songRepository.GetSongByIdandMaterialId(userId, id);
        if (song == null)
        {
            return RedirectToAction("Index");
        }
        return View(song);
    }

    [Authorize]
    // POST: HomeController1/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            var song = new Song();
            song = _songRepository.GetSongByIdandMaterialId(userId, id);
            _songRepository.Delete(song);


            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }


}
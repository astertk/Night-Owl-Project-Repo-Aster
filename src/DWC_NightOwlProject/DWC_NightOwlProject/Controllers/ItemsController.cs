using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

namespace DWC_NightOwlProject.Controllers
{
    
    public  class ItemsController : Controller
    {
        private IItemRepository _itemRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _he;

        public ItemsController(IMaterialRepository materialRepository, IItemRepository itemRepository, IConfiguration config,
                                    UserManager<IdentityUser> userManager, IWebHostEnvironment he)
        {
            _itemRepository = itemRepository;
            _config = config;
            _userManager = userManager;
            _he = he;

        }
        public IActionResult Index()
        {
            var vm = new MaterialVM();
            string id = _userManager.GetUserId(User);
            var result = new List<Item>();
            result = _itemRepository.GetAllItemsById(id);
            vm.items = result;
            return View(vm);
        }
        public async Task<IActionResult> Template(IFormCollection collection)
        {
            var userId = _userManager.GetUserId(User);

            if (_itemRepository.GetAllItemsById(userId).Count() < 4)
            {
                var APIKey = _config["APIKey"];
                var api = new OpenAIClient(new OpenAIAuthentication(APIKey));

                var item = new Item();
                item.Prompt = "Create a DND Item with these parameters: This item has a rarity of " + collection["r0"]
                    + ". The item type is " + collection["r1"]
                    + ". In it's description include the keywords: " + collection["r2"]
                    + ". Make the description 3 sentences long. Do not create the name of the item.";
                item.UserId = userId;
                item.Id = 0;

                var namePrompt = "Create a name for this DND Item. It is a(n) " + collection["r0"]
                    + " " + collection["r1"]
                    + ". In it's description include the keywords: " + collection["r2"] + ". Make the name range from 4 to 7 words.";
                var gptName = await api.CompletionsEndpoint.CreateCompletionAsync(namePrompt, max_tokens: 20, temperature: 1, presencePenalty: 0, frequencyPenalty: 0, model: OpenAI.Models.Model.Davinci);
                item.Name = gptName.ToString();
                item.CreationDate = DateTime.Now;

                var desc = await api.CompletionsEndpoint.CreateCompletionAsync(item.Prompt, max_tokens: 2000, temperature: 1, presencePenalty: 0, frequencyPenalty: 0, model: OpenAI.Models.Model.Davinci);
                item.Completion = desc.ToString();

                var dallePrompt = "Make a DND item art image of this item prompt. Make it look authentic. Don't put any words or letters in the Item art. Just make the Item. The Item is a(n)" + collection["r0"] 
                    + " "
                    + collection["r1"] + ". The keyword of this item is: " + collection["r2"] + ".";
                var dalleList = await api.ImagesEndPoint.GenerateImageAsync(dallePrompt, 1, ImageSize.Large);
                var dalleImage = dalleList.FirstOrDefault();
                var url = dalleImage.ToString();
                //string tempPath = Path.GetTempPath();

                // Scrape web page
                WebClient webClient = new WebClient();
                item.PictureData = webClient.DownloadData(url);
                _itemRepository.AddOrUpdate(item);
            }
            return RedirectToAction("Index", "Items");

        }
        [Authorize]
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var item = new Item();
            item = _itemRepository.GetItemByIdandMaterialId(userId, id);

            return View(item);
        }

        [Authorize]
        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var item = new Item();
            item = _itemRepository.GetItemByIdandMaterialId(userId, id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
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
                var item = new Item();
                item = _itemRepository.GetItemByIdandMaterialId(userId, id);
                _itemRepository.Delete(item);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

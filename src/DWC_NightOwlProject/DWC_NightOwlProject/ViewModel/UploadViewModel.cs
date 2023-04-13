using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace DWC_NightOwlProject.ViewModel
{
    public class UploadViewModel
    {
        public string imagePath { get; set; }  
        public string fileName { get; set; }
        public IFormFile imageFile { get; set; }

        public List<Material> materials { get; set; }

        public List<string> Responses { get; set; }

        public string Prompt { get; set; }
    }
}

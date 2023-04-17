using Microsoft.AspNetCore.Mvc;

namespace DWC_NightOwlProject.Data
{
    public class MaterialVM
    {
        public MaterialVM() { }

        public Material material { get; set; }
        public List<Material> materials { get; set; }

        public List<string> Responses { get; set; }

        public string Prompt { get; set; }

    }

    
}

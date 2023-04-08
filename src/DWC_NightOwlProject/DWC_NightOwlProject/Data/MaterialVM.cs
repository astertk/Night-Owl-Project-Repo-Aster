using Microsoft.AspNetCore.Mvc;

namespace DWC_NightOwlProject.Data
{
    public class MaterialVM
    {
        public MaterialVM() { }
        public List<Material> materials { get; set; }

        public string r0 { get; set; }
        public string r1 { get; set; }
        public string r2 { get; set; }
        public string r3 { get; set; }
        public string r4 { get; set; }


        public string Prompt { get; set; }
 
    }

    
}

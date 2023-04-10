using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;
using System.Drawing;

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

        public Image upload { get; set; }


        public string Prompt { get; set; }

        public string? FileName { get; set; }

        public byte[]? PictureData { get; set; }

    }

    
}

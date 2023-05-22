using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;
using System.Drawing;
using Microsoft.DotNet;


namespace DWC_NightOwlProject.Data
{
    public class MaterialVM
    {
        public MaterialVM() { }

        public Material material { get; set; }
        public List<Material> materials { get; set; }

        public List<Character> characters { get; set; }

        public List<Quest> quests { get; set; }

        public List<Map> maps { get; set; }
        public List<Backstory> backstories{get;set;}
        public List<Encounter> encounters {get;set;}
        public Backstory backstory{get;set;}
        public List<Item> items { get; set; }

        public List<Song> songs { get; set; }

        public string r0 { get; set; }
        public string r1 { get; set; }
        public string r2 { get; set; }
        public string r3 { get; set; }
        public string r4 { get; set; }
        public string r5 { get; set; }

        public IFormFile upload { get; set; }


        public string Prompt { get; set; }

        public string? FileName { get; set; }

        public byte[]? PictureData { get; set; }

    }

    
}

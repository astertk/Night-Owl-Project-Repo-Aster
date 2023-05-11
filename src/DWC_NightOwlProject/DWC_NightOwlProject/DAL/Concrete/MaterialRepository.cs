using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class MaterialRepository: Repository<Material>, IMaterialRepository
    {
    private DbSet<Material> _materials;
    private DbSet<Map> _maps;
    private readonly UserManager<IdentityUser> _userManager;
    

        public MaterialRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _materials = ctx.Materials;
            _userManager = userManager;
            _maps = ctx.Maps;
        }

       

        public Material GetBackstoryById(string userId)
        {
            var result = new Material();
            result = _materials.Where(x => x.UserId == userId).Where(x=>x.Type == "Backstory").FirstOrDefault();

            return result;

        }

        public Material GetQuestById(string userId, int id)
        {
            var result = new Material();
            result = _materials.Where(x => x.UserId == userId).Where(x=>x.Type == "Quest").FirstOrDefault();
            return result;
        }
        public List<Material> GetAllQuestsById(string userId)
        {
            var result = new List<Material>();


            result = _materials.Where(x => x.UserId == userId).Where(x => x.Type == "Quest").ToList();


            return result;

        }



        public Material GetCharacterByIdandMaterialId(string userId, int id) 
        {
            var result = new Material();
            result = _materials.Where(x => x.UserId == userId).Where(x => x.Type == "Character").Where(x=>x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Material> GetAllCharactersById(string userId)
        {
            var result = new List<Material>();


            result = _materials.Where(x => x.UserId == userId).Where(x => x.Type == "Character").ToList();


            return result;

        }

        public List<Material> GetAllEncountersById(string userId)
        {
            var result = new List<Material>();


            result = _materials.Where(x => x.UserId == userId).Where(x => x.Type == "Encounter").ToList();


            return result;

        }

        public Map GetMapByIdandMaterialId(string userId, int id)
        {
            var result = new Map();
            result = _maps.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Map> GetAllMapsById(string userId)
        {
            var result = new List<Map>();


            result = _maps.Where(x => x.UserId == userId).ToList();


            return result;

        }


    }
    
}

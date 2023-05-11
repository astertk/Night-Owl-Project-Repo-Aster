using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class MapRepository : Repository<Map>, IMapRepository
    {

        private DbSet<Map> _maps;
        private readonly UserManager<IdentityUser> _userManager;


        public MapRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            _maps = ctx.Maps;
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

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class EncounterRepository : Repository<Encounter>, IEncounterRepository
    {

        private DbSet<Encounter> encounters;
        private readonly UserManager<IdentityUser> _userManager;


        public EncounterRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            encounters = ctx.Encounters;
        }


        public Encounter GetEncounterByIdandMaterialId(string userId, int id)
        {
            var result = encounters.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Encounter> GetAllEncountersById(string userId)
        {
            var result = encounters.Where(x => x.UserId == userId).ToList();
            return result;
        }


    }

}

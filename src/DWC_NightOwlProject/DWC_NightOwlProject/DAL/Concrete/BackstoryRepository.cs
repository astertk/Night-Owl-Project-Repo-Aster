using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class BackstoryRepository : Repository<Backstory>, IBackstoryRepository
    {

        private DbSet<Backstory> backstories;
        private readonly UserManager<IdentityUser> _userManager;


        public BackstoryRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            backstories = ctx.Backstories;
        }


        public Backstory GetBackstoryByIdandMaterialId(string userId, int id)
        {
            var result = backstories.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Backstory> GetAllBackstoriesById(string userId)
        {
            var result = backstories.Where(x => x.UserId == userId).ToList();
            return result;
        }


    }

}

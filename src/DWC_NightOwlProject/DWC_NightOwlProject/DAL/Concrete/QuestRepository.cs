using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class QuestRepository : Repository<Quest>, IQuestRepository
    {

        private DbSet<Quest> quests;
        private readonly UserManager<IdentityUser> _userManager;


        public QuestRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            quests = ctx.Quests;
        }


        public Quest GetQuestByIdandMaterialId(string userId, int id)
        {
            var result = quests.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Quest> GetAllQuestsById(string userId)
        {
            var result = quests.Where(x => x.UserId == userId).ToList();
            return result;
        }


    }

}

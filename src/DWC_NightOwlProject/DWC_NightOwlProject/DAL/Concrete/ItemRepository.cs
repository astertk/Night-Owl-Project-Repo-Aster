using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;



namespace DWC_NightOwlProject.DAL.Concrete
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private DbSet<Item> _items;
        private readonly UserManager<IdentityUser> _userManager;

        public ItemRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            _items = ctx.Items;
        }

        public Item GetItemByIdandMaterialId(string userId, int id)
        {
            var result = new Item();
            result = _items.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Item> GetAllItemsById(string userId)
        {
            var result = new List<Item>();
            result = _items.Where(x => x.UserId == userId).ToList();
            return result;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IItemRepository : IRepository<Item>
    {
        public Item GetItemByIdandMaterialId(string userId, int id);

        public List<Item> GetAllItemsById(string userId);
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IQuestRepository : IRepository<Quest>
    {
       
        public Quest GetQuestByIdandMaterialId(string userId, int id);

        public List<Quest> GetAllQuestsById(string userId);
    }
}
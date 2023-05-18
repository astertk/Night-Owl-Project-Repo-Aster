using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IBackstoryRepository : IRepository<Backstory>
    {
       
        public Backstory GetBackstoryByIdandMaterialId(string userId, int id);

        public List<Backstory> GetAllBackstoriesById(string userId);
    }
}
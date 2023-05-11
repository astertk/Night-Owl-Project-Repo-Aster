using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IMapRepository : IRepository<Map>
    {
       
        public Map GetMapByIdandMaterialId(string userId, int id);

        public List<Map> GetAllMapsById(string userId);
    }
}
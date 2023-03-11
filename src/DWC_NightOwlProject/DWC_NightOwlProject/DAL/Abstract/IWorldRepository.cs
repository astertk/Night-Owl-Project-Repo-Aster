using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IWorldRepository: IRepository<World>
    {
        public World GetUserWorld(string userId);
    }
}

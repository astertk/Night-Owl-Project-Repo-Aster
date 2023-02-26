using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IMaterialRepository: IRepository<Material>
    {
        public Material GetMaterialByUserId(string userId);
    }
}

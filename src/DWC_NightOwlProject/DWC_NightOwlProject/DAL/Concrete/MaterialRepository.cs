using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class MaterialRepository: Repository<Material>, IMaterialRepository
    {
    private DbSet<Material> _materials;
        public MaterialRepository(WebAppDbContext ctx) : base(ctx)
        {
            _materials = ctx.Materials;
        }
    }
    
}

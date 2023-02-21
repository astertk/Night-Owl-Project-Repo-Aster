using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class WorldRepository: Repository<World>, IWorldRepository
    {
    private DbSet<World> _worlds;
        public WorldRepository(WebAppDbContext ctx) : base(ctx)
        {
            _worlds = ctx.Worlds;
        }

    }
    
}

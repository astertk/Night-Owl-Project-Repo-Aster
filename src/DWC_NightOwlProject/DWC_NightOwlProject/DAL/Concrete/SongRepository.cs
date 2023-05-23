using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class SongRepository : Repository<Song>, ISongRepository
    {

        private DbSet<Song> _songs;
        private readonly UserManager<IdentityUser> _userManager;


        public SongRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            _songs = ctx.Songs;
        }


        public Song GetSongByIdandMaterialId(string userId, int id)
        {
            var result = new Song();
            result = _songs.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Song> GetAllSongsById(string userId)
        {
            var result = new List<Song>();


            result = _songs.Where(x => x.UserId == userId).ToList();


            return result;

        }

        public void Save(Song song, string userId)
        {
            var list = GetAllSongsById(userId);
            if (list.Count < 6)
            {
                _songs.Add(song);
            }
           
        }


    }

}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface ISongRepository : IRepository<Song>
    {

        public Song GetSongByIdandMaterialId(string userId, int id);

        public List<Song> GetAllSongsById(string userId);

        public void Save(Song song, string userId);
    }
}
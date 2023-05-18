using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
    public interface IEncounterRepository : IRepository<Encounter>
    {
       
        public Encounter GetEncounterByIdandMaterialId(string userId, int id);

        public List<Encounter> GetAllEncountersById(string userId);
    }
}
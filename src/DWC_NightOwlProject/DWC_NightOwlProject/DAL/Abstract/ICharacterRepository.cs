using DWC_NightOwlProject.Data;

namespace DWC_NightOwlProject.DAL.Abstract
{
  
    public interface ICharacterRepository : IRepository<Character>
        {
        public Character GetCharacterByIdandMaterialId(string userId, int id);

        public List<Character> GetAllCharactersById(string userId);
    }
}

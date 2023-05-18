using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;
using System.Linq;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {

        private DbSet<Character> _characters;
        private readonly UserManager<IdentityUser> _userManager;


        public CharacterRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _userManager = userManager;
            _characters = ctx.Characters;
        }


        public Character GetCharacterByIdandMaterialId(string userId, int id)
        {
            var result = new Character();
            result = _characters.Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Character> GetAllCharactersById(string userId)
        {
            var result = new List<Character>();


            result = _characters.Where(x => x.UserId == userId).ToList();


            return result;

        }


    }

}

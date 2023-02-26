using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Castle.Core.Logging;

namespace DWC_NightOwlProject.DAL.Concrete
{
    public class MaterialRepository: Repository<Material>, IMaterialRepository
    {
    private DbSet<Material> _materials;
    private readonly UserManager<IdentityUser> _userManager;
        public MaterialRepository(WebAppDbContext ctx, UserManager<IdentityUser> userManager) : base(ctx)
        {
            _materials = ctx.Materials;
            _userManager = userManager;
        }

       

        public string SuggestionOne = "The overall tone is:";
        public string SuggestionTwo = "The villains are:";
        public string SuggestionThree = "The heros are:";
        public string SuggestionFour = "The world is:";


        public string AnswerOne;
        public string AnswerTwo;
        public string AnswerThree;
        public string AnswerFour;


       public Material GetMaterialByUserId(string userId)
        {
            var result = new Material();
            result = _materials.Where(x => x.UserId == userId).FirstOrDefault();

            return result;

        }


    }
    
}

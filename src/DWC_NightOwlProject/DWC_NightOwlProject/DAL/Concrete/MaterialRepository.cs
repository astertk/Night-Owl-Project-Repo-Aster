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

       

        public string SuggestionOne = "The overall tone is:";
        public string SuggestionTwo = "The villains are:";
        public string SuggestionThree = "The heros are:";
        public string SuggestionFour = "The world is:";


        public string AnswerOne;
        public string AnswerTwo;
        public string AnswerThree;
        public string AnswerFour;





    }
    
}

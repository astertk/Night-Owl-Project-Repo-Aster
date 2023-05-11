using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.ViewModel
{
    public class ViewModelWorld
    {
        //public String UserIdString{get;set;}
        public String WorldName{get;set;}

        public World ThisWorld{get;set;}

        public Material Backstory { get;set;}
        public List<Material> quests { get;set;}

        public List<Material> characters { get;set;}

        public List<Map> maps { get;set;}
    }
}
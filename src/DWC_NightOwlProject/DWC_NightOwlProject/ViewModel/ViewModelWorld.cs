using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.ViewModel
{
    public class ViewModelWorld
    {
        //public String UserIdString{get;set;}
        public String WorldName{get;set;}

        public World ThisWorld{get;set;}

        public List<Backstory> Backstory { get;set;}
        public List<Quest> quests { get;set;}

        public List<Character> characters { get;set;}

        public List<Map> maps { get;set;}

        public List<Song> Songs { get;set;}

        public List<Item> items { get;set;}
    }
}
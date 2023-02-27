using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;

namespace DWC_NightOwlProject.ViewModel
{
    public class ViewModelWorld
    {
        //public String UserIdString{get;set;}
        public String WorldName{get;set;}

        private World thisWorld;

        public World getWorld()
        {
            return thisWorld;
        }

        public bool setWorld(IWorldRepository worldRepo, string userid)
        {
            World w=worldRepo.GetUserWorld(userid);
            if(w!=null)
            {
                thisWorld=w;
                return true;
            }
            return false;
        }
    }
}
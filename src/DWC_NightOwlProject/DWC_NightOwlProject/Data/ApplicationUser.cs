
using Microsoft.AspNetCore.Identity;


namespace DWC_NightOwlProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}


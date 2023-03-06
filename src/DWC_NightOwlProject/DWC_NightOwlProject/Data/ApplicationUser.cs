
using Microsoft.AspNetCore.Identity;


namespace DWC_NightOwlProject.Data;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


using Microsoft.AspNetCore.Identity;

namespace RealEstatePlatform.Identity.Models
{
    public class ApplicationUser : IdentityUser
    { 
        public string? Name { get; set; }


    }
}

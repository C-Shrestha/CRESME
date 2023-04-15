using Microsoft.AspNetCore.Identity;

namespace CRESME.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

    }
}

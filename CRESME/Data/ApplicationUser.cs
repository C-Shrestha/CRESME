using Microsoft.AspNetCore.Identity;

namespace CRESME.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Role { get; set; }

        public string? Block { get; set; }

        public string? Course { get; set; }

        public string? Term { get; set; }



    }
}

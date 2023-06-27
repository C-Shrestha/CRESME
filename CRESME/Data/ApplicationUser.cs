using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace CRESME.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Role { get; set; }


        [Required]
        public string? Block { get; set; }


        [Required]
        public string? Course { get; set; }


        [Required]
        public string? Term { get; set; }

        [Required]
        public string? OriginalNID { get; set; }

        [Required]
        public string? OriginalAccount { get; set; }



    }
}

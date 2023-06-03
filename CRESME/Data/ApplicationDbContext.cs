using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRESME.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // this will create a new table called Quiz in the DB
        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<Test> Test { get; set; }

        public DbSet<Attempt> Attempt { get; set; }
    }
}
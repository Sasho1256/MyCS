using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class MyCsDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public MyCsDbContext(DbContextOptions<MyCsDbContext> options) 
            :base(options)
        {

        }

        public MyCsDbContext()
        {

        }

        //dbset
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //fluent api
        }
    }
}

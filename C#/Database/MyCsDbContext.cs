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

        //dbsets
        public DbSet<Client> Clients { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //fluent api
        }
    }
}

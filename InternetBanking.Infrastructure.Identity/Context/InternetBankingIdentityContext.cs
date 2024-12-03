using InternetBanking.Infrastructure.Identity.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Identity.Context
{
    public class InternetBankingIdentityContext
    : IdentityDbContext<ApplicationUser>
    {
        public InternetBankingIdentityContext(DbContextOptions<InternetBankingIdentityContext> options)
        : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");

            modelBuilder.Entity<ApplicationUser>(entity => entity.ToTable(name: "Users"))
                        .Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"))
                        .Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"))
                        .Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
        }
    }
}
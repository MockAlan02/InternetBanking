using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Contexts
{
    public class MainContext
    : DbContext
    {
        public DbSet<BasicUser> Users => Set<BasicUser>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Admin> Admins => Set<Admin>();

        public DbSet<BasicProduct> Products => Set<BasicProduct>();
        public DbSet<CreditCard> CreditCards => Set<CreditCard>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<SavingsAccount> SavingsAccounts => Set<SavingsAccount>();

        public DbSet<Transaction> Transactions => Set<Transaction>();

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                                .HavePrecision(18, 6);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BasicUser>()
                        .HasDiscriminator(p => p.UserType)
                        .HasValue<BasicUser>(UserType.Any)
                        .HasValue<Customer>(UserType.Customer)
                        .HasValue<Admin>(UserType.Admin);

            modelBuilder.Entity<BasicUser>().ToTable("Users");

            modelBuilder.Entity<BasicProduct>()
                        .HasDiscriminator(p => p.ProductType)
                        .HasValue<BasicProduct>(ProductType.Any)
                        .HasValue<CreditCard>(ProductType.CreditCard)
                        .HasValue<Loan>(ProductType.Loan)
                        .HasValue<SavingsAccount>(ProductType.SavingAccount);

            modelBuilder.Entity<BasicProduct>().ToTable("Products");
        }
    }
}
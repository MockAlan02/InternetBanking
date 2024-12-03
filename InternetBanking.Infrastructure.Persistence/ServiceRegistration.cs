using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Contexts;
using InternetBanking.Infrastructure.Persistence.Interceptors;
using InternetBanking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
                                                             IConfiguration config)
        {
            string? connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DefaultConnection cannot be unset in the configuration.");
            }

            services.AddDbContext<MainContext>(options =>
                options.UseSqlServer(connectionString)
                       .AddInterceptors(new UpdateAuditableEntitiesInterceptor())
            );
            return services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                           .AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
                           .AddTransient(typeof(IUserRepository<>), typeof(UserRepository<>))
                           .AddTransient<IAdminRepository, AdminRepository>()
                           .AddTransient<ICustomerRepository, CustomerRepository>()
                           .AddTransient(typeof(IProductRepository<>), typeof(ProductRepository<>))
                           .AddTransient<IBeneficiareRepository, BeneficiarieRepository>()
                           .AddTransient<ISavingsAccountRepository, SavingsAccountRepository>()
                           .AddTransient<ICreditCardRepository, CreditCardRepository>()
                           .AddTransient<ILoanRepository, LoanRepository>()
                           .AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
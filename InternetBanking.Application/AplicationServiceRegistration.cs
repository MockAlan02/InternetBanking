using Microsoft.Extensions.DependencyInjection;
using InternetBanking.Application.Services;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.Mappings;
using System.Reflection;
namespace InternetBanking.Application
{
    public static class AplicationServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBeneficiarieService, BeneficiarieService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<ISavingsAccountService, SavingsAccountService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}

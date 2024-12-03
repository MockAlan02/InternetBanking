using InternetBanking.Application.Dto.Customer;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Repositories
{
    public interface ICustomerRepository: IUserRepository<Customer>
    {
        Task<CustomerCountDTO> CountCustomers();
    }
}

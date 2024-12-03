using InternetBanking.Application.Dto.Customer;
using InternetBanking.Application.Extras;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;


namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : UserRepository<Customer>, ICustomerRepository
    {
        private readonly MainContext _context;

        public CustomerRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public Task<CustomerCountDTO> CountCustomers()
        {
            var customerCounts = _context.Customers
                                         .GroupBy(c => c.IsActive)
                                         .Select(g => new
                                         {
                                             IsActive = g.Key,
                                             Count = g.Count()
                                         })
                                         .ToList();

            CustomerCountDTO customerCountDTO = new()
            {
                ActiveCustomersCount = customerCounts.Where(x => x.IsActive).Select(x => x.Count).FirstOrDefault(),
                UnactiveCustomersCount = customerCounts.Where(x => !x.IsActive).Select(x => x.Count).FirstOrDefault()
            };
            return customerCountDTO.AsTask();
        }
    }
}

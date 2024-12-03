using AutoMapper;

using InternetBanking.Application.Dto.Customer;
using InternetBanking.Application.Dto.User;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class CustomerService: GenericService<CustomerSaveViewModel, CustomerViewModel, Customer, string>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        : base(customerRepository, mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<UserStatisticsDTO> CalculateUserStatistics()
        {
            CustomerCountDTO customerCountDTO = await _customerRepository.CountCustomers();
            return new()
            {
                NumberOfActiveCustomer = customerCountDTO.ActiveCustomersCount,
                NumberOfUnactiveCustomer = customerCountDTO.UnactiveCustomersCount
            };
        }
    }
}

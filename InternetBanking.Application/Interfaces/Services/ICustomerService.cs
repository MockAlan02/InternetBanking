using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.Dto.User;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface ICustomerService: IGenericService<CustomerSaveViewModel, CustomerViewModel, Customer, string>
    {
        Task<UserStatisticsDTO> CalculateUserStatistics();
    }
}

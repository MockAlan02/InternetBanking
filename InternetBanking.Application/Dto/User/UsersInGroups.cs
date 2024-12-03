using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Dto.User
{
    public class UsersInGroupsDTO
    {
        public List<Admin> ActiveAdmins { get; set; } = new List<Admin>();
        public List<Admin> UnactiveAdmins { get; set; } = new List<Admin>();
        public List<Domain.Entities.Customer> ActiveCustomers { get; set; } = new List<Domain.Entities.Customer>();
        public List<Domain.Entities.Customer> UnactiveCustomers { get; set; } = new List<Domain.Entities.Customer>();
    }
}
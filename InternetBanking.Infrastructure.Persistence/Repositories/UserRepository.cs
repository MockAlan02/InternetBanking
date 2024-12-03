using InternetBanking.Application.Dto.User;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class UserRepository<TUser>
    : GenericRepository<TUser, string>, IUserRepository<TUser>
    where TUser : BasicUser
    {
        private readonly MainContext _context;

        public UserRepository(MainContext context)
        : base(context)
        {
            _context = context;
        }

        public Task<UsersInGroupsDTO> GetAllUsersInGroup()
        {
            UsersInGroupsDTO usersInGroupsDTO
                = _context.Admins.AsEnumerable()
                           .Aggregate(new UsersInGroupsDTO(), (acc, it) =>
                           {
                               if (it.IsActive)
                               {
                                   acc.ActiveAdmins.Add(it);
                               }
                               else
                               {
                                   acc.UnactiveAdmins.Add(it);
                               }
                               return acc;
                           });
            usersInGroupsDTO
                = _context.Customers.AsEnumerable()
                           .Aggregate(usersInGroupsDTO, (acc, it) =>
                           {
                               if (it.IsActive)
                               {
                                   acc.ActiveCustomers.Add(it);
                               }
                               else
                               {
                                   acc.UnactiveCustomers.Add(it);
                               }
                               return acc;
                           });
            return Task.FromResult(usersInGroupsDTO);
        }
    }
}
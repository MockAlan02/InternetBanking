using InternetBanking.Application.Dto.User;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Repositories
{
    public interface IUserRepository<TUser>
    : IGenericRepository<TUser, string>
    where TUser : BasicUser
    {
        Task<UsersInGroupsDTO> GetAllUsersInGroup();
    }
}
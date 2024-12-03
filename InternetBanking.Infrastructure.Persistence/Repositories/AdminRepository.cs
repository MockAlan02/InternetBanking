using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class AdminRepository: UserRepository<Admin>, IAdminRepository
    {
        private readonly MainContext _context;

        public AdminRepository(MainContext context): base(context)
        {
            _context = context;
        }

    }
}

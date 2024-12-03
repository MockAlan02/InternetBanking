using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class SavingsAccountRepository: ProductRepository<SavingsAccount>, ISavingsAccountRepository
    {
        private readonly MainContext _context;

        public SavingsAccountRepository(MainContext context) : base(context)
        {
            _context = context;
        }
    }
}

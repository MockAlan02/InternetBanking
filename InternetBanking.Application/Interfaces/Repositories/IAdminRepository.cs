using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Repositories
{
    public interface IAdminRepository: IUserRepository<Admin>
    {
    }
}

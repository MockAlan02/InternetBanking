using InternetBanking.Application.ViewModels.Admin;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface IAdminService:IGenericService<AdminSaveViewModel, AdminViewModel, Admin, string>
    {
    }
}

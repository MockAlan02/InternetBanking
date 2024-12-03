using AutoMapper;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Admin;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class AdminService: GenericService<AdminSaveViewModel, AdminViewModel, Admin, string>, IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository _adminRepository, IMapper _mapper): base(_adminRepository, _mapper)
        {
            this._adminRepository = _adminRepository;
            this._mapper = _mapper;
        }
    }
}

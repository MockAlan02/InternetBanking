using AutoMapper;

using InternetBanking.Application.Dto.Login;
using InternetBanking.Application.Dto.Register;
using InternetBanking.Application.Dto.User;
using InternetBanking.Application.ViewModels.Admin;
using InternetBanking.Application.ViewModels.CreditCard;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Application.ViewModels.Loan;
using InternetBanking.Application.ViewModels.SavingsAccount;
using InternetBanking.Application.ViewModels.User;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CustomerSaveViewModel, RegisterRequest>().ReverseMap();
            CreateMap<AdminSaveViewModel, RegisterRequest>().ReverseMap();
            CreateMap<AdminSaveViewModel, RegisterViewModel>().ReverseMap();
            CreateMap<CustomerSaveViewModel, RegisterViewModel>().ReverseMap();
            CreateMap<LoginViewModel, LoginRequest>().ReverseMap();

            CreateMap<Customer, BaseUserViewModel>();
            CreateMap<Admin, BaseUserViewModel>();
            CreateMap<UsersInGroupsDTO, UsersInGroupsViewModel>();
            CreateMap<AdminSaveViewModel, Admin>().ReverseMap();
            CreateMap<CustomerSaveViewModel, Customer>().ReverseMap();

            CreateMap<SavingsAccount, SavingsAccountSaveViewModel>().ReverseMap();
            CreateMap<LoanSaveViewModel, Loan>().ReverseMap();
            CreateMap<CreditCardSaveViewModel, CreditCard>().ReverseMap();
        }
    }
}

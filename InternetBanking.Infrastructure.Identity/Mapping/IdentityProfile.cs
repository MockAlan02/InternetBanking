using AutoMapper;

using InternetBanking.Application.Dto.Register;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Infrastructure.Identity.Mapping
{
    public class IdentityProfile
    : Profile
    {
        public IdentityProfile()
        {
            CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
        }
    }
}
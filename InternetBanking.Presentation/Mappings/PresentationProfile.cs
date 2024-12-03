using AutoMapper;

using InternetBanking.Application.Dto.Login;
using InternetBanking.Presentation.DTO;

namespace InternetBanking.Presentation.Mappings
{
    internal class PresentationProfile
    : Profile
    {
        public PresentationProfile()
        {
            CreateMap<LoginResponse, LoginInfoDTO>();
        }
    }
}
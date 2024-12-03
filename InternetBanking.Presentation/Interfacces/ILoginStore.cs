using InternetBanking.Presentation.DTO;

namespace InternetBanking.Presentation.Interfacces
{
    public interface ILoginStore
    {
        Task Put(LoginInfoDTO info);
        Task<LoginInfoDTO> Get();
        Task<T> Gets<T>(Func<LoginInfoDTO, T> accesor);
        Task Clear();
    }
}
using System.Text.Json;

using InternetBanking.Application.Extras;
using InternetBanking.Presentation.DTO;
using InternetBanking.Presentation.Interfacces;

namespace InternetBanking.Presentation.Impl
{
    public class SessionLogginStore
    : ILoginStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string UserKey = "LOGIN";

        public SessionLogginStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Put(LoginInfoDTO info)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new InvalidDataException("Acessing http context when not avalible");
            }
            string v = JsonSerializer.Serialize(info);
            _httpContextAccessor.HttpContext.Session.SetString(UserKey, v);
            return Task.CompletedTask;
        }

        public Task<LoginInfoDTO> Get()
        {
            string? v = _httpContextAccessor.HttpContext?.Session.GetString(UserKey);
            if (v is null)
            {
                throw new InvalidDataException("Acessing http context when not avalible");
            }
            return JsonSerializer.Deserialize<LoginInfoDTO>(v)
                                ?.AsTask() ?? throw new InvalidDataException("Found null in login information");
        }

        public async Task<T> Gets<T>(Func<LoginInfoDTO, T> accesor)
        => accesor(await Get());

        public Task Clear()
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new InvalidDataException("Acessing http context when not avalible");
            }
            _httpContextAccessor.HttpContext?.Session.Remove(UserKey);
            return Task.CompletedTask;
        }
    }
}
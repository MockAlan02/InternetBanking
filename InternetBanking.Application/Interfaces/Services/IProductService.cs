using InternetBanking.Application.Dto.Product;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface IProductService<TSaveViewModel, TViewModel, TProduct>
    : IGenericService<TSaveViewModel, TViewModel, TProduct>
    where TSaveViewModel : class
    where TViewModel : class
    where TProduct : BasicProduct
    {
        Task<TViewModel?> FindByNumberAccount(string accountNumber);
        Task RemoveByAccountNumber(string identifierAccount);
    }

    public interface IProductService
    {
        Task<ProductsStatisticsDTO> CalculateStatistics();
    }
}

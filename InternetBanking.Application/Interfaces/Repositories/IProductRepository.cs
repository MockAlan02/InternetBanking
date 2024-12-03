using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Repositories
{
    public interface IProductRepository<TProduct> : IGenericRepository<TProduct>
    where TProduct : BasicProduct
    {
        /// <summary>
        /// Takes a list of identifiers and returns a list of avalible identifiers
        /// </summary>
        Task<string[]> FilterWithExistingIdentifiers(string[] identifiers);
        Task<TProduct?> FindByNumberAccount(string accountNumber);
        Task RemoveByAccountNumber(string identitifierAccount);
        Task<int> CountProducts();
    }
}

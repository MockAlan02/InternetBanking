using System.Linq.Expressions;

using InternetBanking.Application.Extras;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;

using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class ProductRepository<TProduct>
    : GenericRepository<TProduct>,
      IProductRepository<TProduct>
    where TProduct : BasicProduct
    {
        private readonly MainContext _context;

        public ProductRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public Task<int> CountProducts()
        {
            return _context.Set<TProduct>().Count().AsTask();
        }

        public Task<string[]> FilterWithExistingIdentifiers(string[] identifiers)
        {
            IEnumerable<string> alreadyExisting =
                _context.Products.Select(b => b.IdentifierAccount)
                                 .Where(i => identifiers.Contains(i))
                                 .AsEnumerable();
            return Task.FromResult(
                identifiers.Except(
                    alreadyExisting
                ).ToArray()
            );
        }

        public async Task<TProduct?> FindByNumberAccount(string accountNumber)
        {
            return await FindByProperty(p => p.IdentifierAccount == accountNumber)!;
        }

        public async Task RemoveByAccountNumber(string identitifierAccount)
        {
            var productAccount = await FindByNumberAccount(identitifierAccount);
            var product = await _context.Products.FindAsync(productAccount!.Id);
            if (product != null)
            {
                _context.Remove(product);
            }
        }

        protected async Task<TProduct?> FindByProperty(Expression<Func<TProduct, bool>> predicate)
        {
            return await _context.Set<TProduct>().FirstOrDefaultAsync(predicate)!;
        }
    }
}
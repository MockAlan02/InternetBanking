using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Domain.Common;

namespace InternetBanking.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, TKey id);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
    }

    public interface IGenericRepository<TEntity>
    : IGenericRepository<TEntity, int>
    where TEntity : BaseEntity<int>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Common;
using InternetBanking.Infrastructure.Persistence.Contexts;

using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>
    : IGenericRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    {
        private readonly MainContext _context;

        public GenericRepository(MainContext context)
        {
            _context = context;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, TKey id)
        {
            var entry = await _context.Set<TEntity>().FindAsync(id);
            if (entry is null) return;
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }

    public class GenericRepository<TEntity>
    : GenericRepository<TEntity, int>,
      IGenericRepository<TEntity>
    where TEntity : BaseEntity<int>
    {
        public GenericRepository(MainContext context) : base(context)
        {
        }
    }
}

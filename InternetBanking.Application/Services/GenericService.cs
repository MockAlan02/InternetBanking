using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Domain.Common;

namespace InternetBanking.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, TEntity, TKey>
    : IGenericService<SaveViewModel, ViewModel, TEntity, TKey>
    where SaveViewModel:class
    where ViewModel : class
    where TEntity : BaseEntity<TKey>
    {
        private readonly IGenericRepository<TEntity, TKey> _genericRepository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity, TKey> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            TEntity data = _mapper.Map<TEntity>(vm);
            await _genericRepository.AddAsync(data);
            return vm;
        }

        public virtual async Task Delete(TKey id)
        {
            TEntity? entity = await _genericRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return;
            }

            await _genericRepository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            List<TEntity> entities = await _genericRepository.GetAllAsync();
            return _mapper.Map<List<TEntity>, List<ViewModel>>(entities);
        }

        public virtual async Task<SaveViewModel?> GetByIdSaveViewModel(TKey id)
        {
            TEntity? entity = await _genericRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                return _mapper.Map<SaveViewModel?>(entity);
            }
            return null;
        }

        public virtual async Task Update(SaveViewModel vm, TKey id)
        {
            TEntity entity = _mapper.Map<TEntity>(vm);
            await _genericRepository.UpdateAsync(entity, id);
        }
    }

    public class GenericService<SaveViewModel, ViewModel, TEntity>
    : GenericService<SaveViewModel, ViewModel, TEntity, int>,
      IGenericService<SaveViewModel, ViewModel, TEntity>
    where SaveViewModel : class
    where ViewModel : class
    where TEntity : BaseEntity<int>
    {
        public GenericService(IGenericRepository<TEntity, int> genericRepository, IMapper mapper)
        : base(genericRepository, mapper)
        {
        }
    }
}

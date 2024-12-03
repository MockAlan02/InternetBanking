using InternetBanking.Domain.Common;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface IGenericService<TSaveViewModel, TViewModel, TEntity, TKey>
    where TSaveViewModel: class
    where TViewModel : class
    where TEntity : BaseEntity<TKey>
    {
        Task Update(TSaveViewModel vm, TKey id);

        Task<TSaveViewModel> Add(TSaveViewModel vm);

        Task Delete(TKey id);

        Task<TSaveViewModel?> GetByIdSaveViewModel(TKey id);

        Task<List<TViewModel>> GetAllViewModel();
    }
    public interface IGenericService<TSaveViewModel, TViewModel, TEntity> : IGenericService<TSaveViewModel,TViewModel,TEntity,int>
    where TSaveViewModel: class
    where TViewModel : class
    where TEntity : BaseEntity<int>
    {
    }
}



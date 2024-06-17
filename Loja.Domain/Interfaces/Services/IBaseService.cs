using Loja.Domain.Entities;

namespace Loja.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Get();

        Task<TEntity> Get(int id);

        Task<TEntity> Add(TEntity entity);

        Task Edit(TEntity entity);

        Task Delete(int id);

        Task HardDelete(int id);
    }
}
using Loja.Domain.Entities;

namespace Loja.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Read();

        Task<TEntity> Read(int id);

        Task<TEntity> Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);

        Task HardDelete(int id);
    }
}

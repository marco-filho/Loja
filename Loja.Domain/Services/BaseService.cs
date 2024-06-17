using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repositories;
using Loja.Domain.Interfaces.Services;

namespace Loja.Domain.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> Get() => await _repository.Read();

        public virtual async Task<TEntity> Get(int id) => await _repository.Read(id);

        public virtual async Task<TEntity> Add(TEntity entity) => await _repository.Create(entity);

        public virtual async Task Edit(TEntity entity) => await _repository.Update(entity);

        public virtual async Task Delete(int id) => await _repository.Delete(id);

        public virtual async Task HardDelete(int id) => await _repository.HardDelete(id);
    }
}
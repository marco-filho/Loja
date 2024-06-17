using Loja.Domain.Entities;
using Loja.Domain.Exceptions;
using Loja.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        public readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<IEnumerable<TEntity>> Read()
        {
            try
            {
                var entities = await _context
                    .Set<TEntity>()
                    .IgnoreDeleted()
                    .AsNoTracking()
                    .ToListAsync();

                return entities;
            }
            catch (Exception e)
            {
                throw new PersistenceException(e);
            }
        }

        public virtual async Task<TEntity> Read(int id)
        {
            try
            {
                var entity = await _context
                    .Set<TEntity>()
                    .IgnoreDeleted()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);

                return entity;
            }
            catch (Exception e)
            {
                throw new PersistenceException(e);
            }
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                var result = await _context.Set<TEntity>().AddAsync(entity);

                await _context.SaveChangesAsync();

                entity = result.Entity;

                return entity;
            }
            catch (Exception e)
            {
                throw new PersistenceException(e);
            }
        }

        public virtual async Task Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistenceException(e);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                TEntity entity = await Read(id);

                entity.SoftDelete();

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistenceException(e);
            }
        }

        public async Task HardDelete(int id)
        {
            try
            {
                TEntity entity = await Read(id);

                _context.Set<TEntity>().Remove(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistenceException(e);
            }
        }
    }

    static class DbSetExtensions
    {
        public static IQueryable<TEntity> IgnoreDeleted<TEntity>(
            this DbSet<TEntity> set) where TEntity : Entity => set.Where(e => e.DeletedAt == null);
    }
}

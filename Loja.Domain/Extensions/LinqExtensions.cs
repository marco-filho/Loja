using System.Linq.Expressions;

namespace Loja.Domain.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> AddPredicateOptional<TEntity>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> predicate,
            bool condition) where TEntity : class
        {
            if (condition)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public static IQueryable<TEntity> AddPredicate<TEntity>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            query = query.Where(predicate);

            return query;
        }
    }
}

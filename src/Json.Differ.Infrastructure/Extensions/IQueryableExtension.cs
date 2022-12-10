using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Json.Differ.Infrastructure.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> ApplyIncludes<TEntity>(this IQueryable<TEntity> queryable, params string[] includes) where TEntity : class
        {
            if (includes == null)
                return queryable;

            var objectQuery = queryable as IQueryable<TEntity>;
            if (objectQuery != null)
            {
                foreach (var include in includes)
                    objectQuery = objectQuery.Include(include);

                return objectQuery;
            }
            else
                return queryable;
        }

        public static IQueryable<TEntity> ApplyPredicates<TEntity>(this IQueryable<TEntity> queryable, params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            if (predicates == null)
                return queryable;

            var objectQuery = queryable as IQueryable<TEntity>;
            if (objectQuery != null)
            {
                foreach (var predicate in predicates)
                    objectQuery = objectQuery.Where(predicate);

                return objectQuery;
            }
            else
                return queryable;
        }
    }
}
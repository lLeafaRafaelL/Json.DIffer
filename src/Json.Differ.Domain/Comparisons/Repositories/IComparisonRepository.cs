using Json.Differ.Core.Models.Repository;
using Json.Differ.Domain.Comparisons.Models;
using System.Linq.Expressions;

namespace Json.Differ.Domain.Comparisons.Repositories
{
    public interface IComparisonRepository : IUnityOfWorkAsyncRepository
    {
        ValueTask AddAsync(Comparison comparison, CancellationToken cancellationToken);
        void Update(Comparison comparison, CancellationToken cancellationToken);
        void Delete(Comparison comparison, CancellationToken cancellationToken);
        ValueTask<Comparison> GetAsync(Expression<Func<Comparison, bool>> predicate, CancellationToken cancellationToken, params string[] includes);
        ValueTask<IList<Comparison>> FindAsync(Expression<Func<Comparison, bool>> predicate, CancellationToken cancellationToken, params string[] includes);
        ValueTask<IList<Comparison>> FindAsync(CancellationToken cancellationToken, Expression<Func<Comparison, bool>>[] predicates, params string[] includes);
    }
}
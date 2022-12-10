using Json.Differ.Core.Data.EntityFrameworkCore;
using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Comparisons.Repositories;
using Json.Differ.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Json.Differ.Infrastructure.Domain.Comparisons.Repositories
{
    internal class ComparisonRepository : EFUnityOfWorkAsyncRepository, IComparisonRepository
    {
        public ComparisonRepository(EFUnityOfWorkAsync efUnityOfWork) : base(efUnityOfWork)
        {

        }

        public async ValueTask AddAsync(Comparison comparison, CancellationToken cancellationToken)
        {
            await EfUnityOfWorkImpl.Set<Comparison>()
                                   .AddAsync(comparison ?? throw new ArgumentNullException(nameof(comparison)), cancellationToken);
        }

        public void Update(Comparison comparison, CancellationToken cancellationToken)
        {
            EfUnityOfWorkImpl.Set<Comparison>()
                             .Update(comparison ?? throw new ArgumentNullException(nameof(comparison)));
        }

        public void Delete(Comparison comparison, CancellationToken cancellationToken)
        {
            EfUnityOfWorkImpl.Set<Comparison>()
                             .Remove(comparison ?? throw new ArgumentNullException(nameof(comparison)));
        }

        public async ValueTask<IList<Comparison>> FindAsync(Expression<Func<Comparison, bool>> predicate, CancellationToken cancellationToken, params string[] includes)
        {
            return await EfUnityOfWorkImpl.Set<Comparison>()
                                          .Where(predicate)
                                          .ApplyIncludes(includes)
                                          .ToListAsync(cancellationToken);
        }

        public async ValueTask<IList<Comparison>> FindAsync(CancellationToken cancellationToken, Expression<Func<Comparison, bool>>[] predicates, params string[] includes)
        {
            return await EfUnityOfWorkImpl.Set<Comparison>()
                                          .ApplyIncludes(includes)
                                          .ApplyPredicates(predicates)
                                          .ToListAsync(cancellationToken);
        }

        public async ValueTask<Comparison> GetAsync(Expression<Func<Comparison, bool>> predicate, CancellationToken cancellationToken, params string[] includes)
        {
            return await EfUnityOfWorkImpl.Set<Comparison>()
                                          .ApplyIncludes(includes)
                                          .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
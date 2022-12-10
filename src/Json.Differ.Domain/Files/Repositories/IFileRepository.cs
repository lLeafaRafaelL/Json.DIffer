using Json.Differ.Core.Models.Repository;
using Json.Differ.Domain.Files.Models;
using System.Linq.Expressions;

namespace Json.Differ.Domain.Files.Repositories
{
    public interface IFileRepository : IUnityOfWorkAsyncRepository
    {
        ValueTask AddAsync(FileToCompare comparison, CancellationToken cancellationToken);
        void Update(FileToCompare comparison, CancellationToken cancellationToken);
        void Delete(FileToCompare comparison, CancellationToken cancellationToken);
        ValueTask<FileToCompare> GetAsync(Expression<Func<FileToCompare, bool>> predicate, CancellationToken cancellationToken, params string[] includes);
        ValueTask<IList<FileToCompare>> FindAsync(Expression<Func<FileToCompare, bool>> predicate, CancellationToken cancellationToken, params string[] includes);
        ValueTask<IList<FileToCompare>> FindAsync(CancellationToken cancellationToken, Expression<Func<FileToCompare, bool>>[] predicates, params string[] includes);
    }
}
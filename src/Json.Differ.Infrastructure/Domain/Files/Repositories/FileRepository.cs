using Json.Differ.Core.Data.EntityFrameworkCore;
using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Repositories;
using Json.Differ.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Json.Differ.Infrastructure.Domain.Files.Repositories
{
    internal class FileRepository : EFUnityOfWorkAsyncRepository, IFileRepository
    {
        public FileRepository(EFUnityOfWorkAsync efUnityOfWork) : base(efUnityOfWork)
        {

        }

        public async ValueTask AddAsync(FileToCompare file, CancellationToken cancellationToken)
        {
            await EfUnityOfWorkImpl.Set<FileToCompare>()
                                   .AddAsync(file ?? throw new ArgumentNullException(nameof(file)), cancellationToken);
        }

        public void Update(FileToCompare file, CancellationToken cancellationToken)
        {
            EfUnityOfWorkImpl.Set<FileToCompare>()
                             .Update(file ?? throw new ArgumentNullException(nameof(file)));
        }

        public void Delete(FileToCompare file, CancellationToken cancellationToken)
        {
            EfUnityOfWorkImpl.Set<FileToCompare>()
                             .Remove(file ?? throw new ArgumentNullException(nameof(file)));
        }

        public async ValueTask<IList<FileToCompare>> FindAsync(Expression<Func<FileToCompare, bool>> predicate, CancellationToken cancellationToken, params string[] includes)
        {
            return await EfUnityOfWorkImpl.Set<FileToCompare>()
                                          .Where(predicate)
                                          .ApplyIncludes(includes)
                                          .ToListAsync(cancellationToken);
        }

        public async ValueTask<IList<FileToCompare>> FindAsync(CancellationToken cancellationToken, Expression<Func<FileToCompare, bool>>[] predicates, params string[] includes)
        {
            return await EfUnityOfWorkImpl.Set<FileToCompare>()
                                          .ApplyIncludes(includes)
                                          .ApplyPredicates(predicates)
                                          .ToListAsync(cancellationToken);
        }

        public async ValueTask<FileToCompare> GetAsync(Expression<Func<FileToCompare, bool>> predicate, CancellationToken cancellationToken, params string[] includes)
        {
            return await EfUnityOfWorkImpl.Set<FileToCompare>()
                                          .ApplyIncludes(includes)
                                          .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
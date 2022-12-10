using Json.Differ.Core.Models;

namespace Json.Differ.Core.Data
{
    public interface IUnityOfWorkBase
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void EnsureIsAttached<TEntity>(TEntity entity) where TEntity : Entity;
        void SetModified<TEntity>(TEntity entity) where TEntity : Entity;
    }
}

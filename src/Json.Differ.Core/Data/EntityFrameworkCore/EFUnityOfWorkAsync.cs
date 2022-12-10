using Json.Differ.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Json.Differ.Core.Data.EntityFrameworkCore
{
    public sealed class EFUnityOfWorkAsync : IUnityOfWorkAsync
    {
        private EFUnityOfWorkAsyncTransaction _currentTransaction;

        public EFUnityOfWorkAsync(DbContext unityOfworkImpl) =>
            UnityOfWorkImpl = unityOfworkImpl ?? throw new ArgumentNullException(nameof(unityOfworkImpl));

        public DbContext UnityOfWorkImpl { get; private set; }

        public EFUnityOfWorkAsyncTransaction CurrentTransaction =>
            _currentTransaction != null && !_currentTransaction.Completed
            ? _currentTransaction
            : null;

        IUnityOfWorkAsyncTransaction IUnityOfWorkAsync.CurrentTransaction => throw new NotImplementedException();

        public async Task<IUnityOfWorkAsyncTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken)
        {
            EnsureDoesntHavePendingTransaction();
            return (_currentTransaction = new EFUnityOfWorkAsyncTransaction(
                await UnityOfWorkImpl.Database.BeginTransactionAsync(cancellationToken)));
        }

        public async Task<IUnityOfWorkAsyncTransaction> BeginTransactionAsync(
            IsolationLevel isolation, CancellationToken cancellationToken)
        {
            EnsureDoesntHavePendingTransaction();
            return (_currentTransaction = new EFUnityOfWorkAsyncTransaction(
                await UnityOfWorkImpl.Database.BeginTransactionAsync(isolation, cancellationToken)));
        }

        public EFUnityOfWorkAsyncTransaction EnsureCurrentTransaction()
        {
            if (CurrentTransaction == null)
                throw new Exception("Transação não iniciada.");

            return CurrentTransaction;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
            UnityOfWorkImpl.SaveChangesAsync(cancellationToken);


        public void EnsureIsAttached<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var entityEntry = UnityOfWorkImpl.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
                UnityOfWorkImpl.Attach(entity);
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            EnsureIsAttached(entity);
            var entityEntry = UnityOfWorkImpl.Entry(entity);
            entityEntry.State = EntityState.Modified;
        }

        private void EnsureDoesntHavePendingTransaction()
        {
            if (CurrentTransaction != null)
                throw new Exception("Transação anterior não concluída.");
        }
    }
}
using Microsoft.EntityFrameworkCore.Storage;

namespace Json.Differ.Core.Data.EntityFrameworkCore
{
    public sealed class EFUnityOfWorkAsyncTransaction : IUnityOfWorkAsyncTransaction
    {
        private readonly IDbContextTransaction _efTransaction;

        public EFUnityOfWorkAsyncTransaction(IDbContextTransaction efTransaction) =>
            _efTransaction = efTransaction;

        public bool Completed { get; private set; }

        public Guid? TransactionId => _efTransaction.TransactionId;

        public async Task CommitAsync(CancellationToken token)
        {
            EnsureTransactionHasntFinished();
            try { await _efTransaction.CommitAsync(token); }
            finally { Completed = true; }
        }

        public async Task RollbackAsync(CancellationToken token)
        {
            EnsureTransactionHasntFinished();
            try { await _efTransaction.RollbackAsync(token); }
            finally { Completed = true; }
        }
        public async ValueTask DisposeAsync()
        {
            try { await _efTransaction.DisposeAsync(); }
            finally { Completed = true; }
        }

        private void EnsureTransactionHasntFinished()
        {
            if (Completed) throw new Exception("Transação já finalizada.");
        }
    }
}